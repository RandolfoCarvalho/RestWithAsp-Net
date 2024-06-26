﻿using Microsoft.EntityFrameworkCore;
using RestWithAspNet.Data;
using RestWithAspNet.Business;
using RestWithAspNet.Business.Implementations;
using MySqlConnector;
using EvolveDb;
using Serilog;
using RestWithAspNet.Repository.Generic;
using Microsoft.Net.Http.Headers;
using RestWithAspNet.Hypermedia.Filters;
using RestWithAspNet.Hypermedia.Enricher;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Rewrite;
using RestWithAspNet.Services;
using RestWithAspNet.Services.Implementations;
using RestWithAspNet.Repository;
using RestWithAspNet.Configurations;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using RestWithAspNet.Hypermedia.Abstract;
using RestWithAspNet.Hypermedia;
using RestWithAspNet.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var tokenConfigurations = new TokenConfiguration();
new ConfigureFromConfigurationOptions<TokenConfiguration>(
    builder.Configuration.GetSection("TokenConfigurations")

    )
    .Configure(tokenConfigurations);

// Configuração da autenticação JWT
builder.Services.AddSingleton(tokenConfigurations);
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = tokenConfigurations.Issuer,
            ValidAudience = tokenConfigurations.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfigurations.Secret))
        };
    });

builder.Services.AddAuthorization(auth =>
{
    auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser().Build());
});
builder.Services.AddCors(options => options.AddDefaultPolicy(builder =>
{
    builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
}));
builder.Services.AddControllers();
//conexão com o banco de dados

var connection = builder.Configuration["ConnectionStrings:DefaultConnection"];
builder.Services.AddDbContext<MysqlContext>(options => options.UseMySql(connection, 
    new MySqlServerVersion(
        new Version(8,0,0))));

if(builder.Environment.IsDevelopment())
{
    MigrateDataBase(connection);
}

var filterOptions = new HyperMediaFilterOptions();
filterOptions.ContentResponseEnricherList.Add(new PersonEnricher());
filterOptions.ContentResponseEnricherList.Add(new BookEnricher());
builder.Services.AddSingleton(filterOptions);

//indejação de dependencia
builder.Services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
builder.Services.AddApiVersioning();
builder.Services.AddScoped<IBookBusiness, BookBusinessImplementation>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddTransient<ILoginBusiness, LoginBusinessImplementation>();
builder.Services.AddScoped<IResponseEnricher, PersonEnricher>();
builder.Services.AddScoped<HyperMediaFilter>();


builder.Services.AddMvc(options =>
{
    options.RespectBrowserAcceptHeader = true;

    options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/xml"));
    options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));
}).AddXmlSerializerFormatters();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "Rest API with Azure and Docker",
            Version = "v1",
            Description = "API RESTFul developed in course 'Rest API with Azure and Docker'",
            Contact = new OpenApiContact
            {
                Name = "Randolfo Carvalho",
                Url = new Uri("https://github.com/RandolfoCarvalho")
            }
        });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
app.UseCors();
app.MapControllerRoute("DefaultApi", "{controller=values}/{id?}");
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json",
        "Rest API with Azure and Docker");
});
var option = new RewriteOptions();
option.AddRedirect("^$", "swagger");
app.UseRewriter(option);

app.Run();

void MigrateDataBase(string? connection)
{
    try
    {
        var evolveConnection = new MySqlConnection(connection);
        var evolve = new Evolve(evolveConnection, Log.Information)
        {
            Locations = new List<string> { "db/migrations", "db/dataset" },
            IsEraseDisabled = true,
        };
        evolve.Migrate();
    }
    catch (Exception e)
    {
        Log.Error("Database migration failed " + e);
        throw;
    }
}