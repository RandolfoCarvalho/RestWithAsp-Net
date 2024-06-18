using RestWithAspNet.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestWithAspNet.Model
{
    //para dar certo (bind) com a tabela do banco de dados
    [Table("Person")]
    public class Person : BaseEntity
    {
        [Column("first_name")]
        public string FirstName { get; set; } 
        [Column("last_name")]
        public string LastName { get; set; } 
        [Column("address")]
        public string Adress { get; set; } 
        [Column("gender")]
        public string Gender { get; set; }
        [Column("enabled")]
        public bool? Enabled { get; set; }
    }
}
