using Microsoft.EntityFrameworkCore;
using RestWithAspNet.Data;
using RestWithAspNet.Services;
using RestWithAspNet.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//conexão com o banco de dados

var connection = builder.Configuration["ConnectionStrings:DefaultConnection"];
builder.Services.AddDbContext<MysqlContext>(options => options.UseMySql(connection, 
    new MySqlServerVersion(
        new Version(8,0,0))));

//indejação de dependencia
builder.Services.AddScoped<IPersonService, PersonServiceImplementation>();
builder.Services.AddApiVersioning();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
