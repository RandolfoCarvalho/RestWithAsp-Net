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
        //por questões de legibilidade ficará "persons"
        public DbSet<Person> Persons { get; set; }
    }
}
