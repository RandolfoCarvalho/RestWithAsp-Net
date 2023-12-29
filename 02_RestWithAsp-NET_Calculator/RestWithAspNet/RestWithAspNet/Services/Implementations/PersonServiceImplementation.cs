using RestWithAspNet.Model;
using System.Security.Cryptography;

namespace RestWithAspNet.Services.Implementations
{
    public class PersonServiceImplementation : IPersonService
    {

        private volatile int count;

        public Person Create(Person person)
        {
            return person;
        }

        public void Delete(long id)
        {
           
        }

        public List<Person> FindAll()
        {
            List<Person> people = new List<Person>();
            for (int i = 0; i < 8; i++)
            {
                Person person = MockPerson(i);
                people.Add(person);
            }
            return people;
        }
        public Person FindById(long id)
        {
            //logica para puxar do banco de dados
            return new Person
            {
                Id = IncrementAndGet(),
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

        private Person MockPerson(int i)
        {
            return new Person
            {
                Id = IncrementAndGet(),
                FirstName = "Person Name" + i,
                LastName = "Person LastName" + i,
                Adress = "Some Adress" + i,
                Gender = "Male"
            };
        }

        private long IncrementAndGet()
        {
            return Interlocked.Increment(ref count);
        }
    }
}
