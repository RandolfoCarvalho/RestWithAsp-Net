using Microsoft.EntityFrameworkCore;
using RestWithAspNet.Model;

namespace RestWithAspNet.Data
{
    public class MysqlContext : DbContext
    {
        public MysqlContext() { }
        public MysqlContext(DbContextOptions<MysqlContext> options) : base (options) 
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connection = "server=localhost;userid=root;password=;database=rest_with_aspnet";
                optionsBuilder.UseMySql(connection,
                    new MySqlServerVersion(
                        new Version(8, 0, 0)));
            }
        }
        //por questões de legibilidade ficará "persons"
        public DbSet<Person> Persons { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
