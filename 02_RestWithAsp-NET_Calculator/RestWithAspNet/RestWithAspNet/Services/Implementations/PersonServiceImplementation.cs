using RestWithAspNet.Data;
using RestWithAspNet.Model;
using System.Security.Cryptography;

namespace RestWithAspNet.Services.Implementations
{
    public class PersonServiceImplementation : IPersonService
    {

        private MysqlContext _context;

        private volatile int count;
        public PersonServiceImplementation(MysqlContext context) 
        {
            _context = context;
        }

        public Person Create(Person person)
        {
            return person;
        }

        public void Delete(long id)
        {
           
        }
        public List<Person> FindAll()
        {
            //por questões de legibilidade ficará "persons"
            return _context.Persons.ToList();
        }
        public Person FindById(long id)
        {
            //logica para puxar do banco de dados
            return new Person
            {
                Id = 1,
                FirstName = "Randolfo",
                LastName = "Irades",
                Adress = "Catalao - Goias Brasil",
                Gender = "Male"
            };
        }

        public Person Update(Person person)
        {
            //logica para atualizar
            return person;
        }
    }
}
