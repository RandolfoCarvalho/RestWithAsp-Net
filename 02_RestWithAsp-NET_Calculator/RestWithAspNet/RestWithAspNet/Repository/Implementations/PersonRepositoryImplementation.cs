using RestWithAspNet.Data;
using RestWithAspNet.Model;
using System;
using System.Security.Cryptography;

namespace RestWithAspNet.Repository.Implementations
{
    public class PersonRepositoryImplementation : IPersonRepository
    {

        private MysqlContext _context;

        public PersonRepositoryImplementation(MysqlContext context) 
        {
            _context = context;
        }

        public List<Person> FindAll()
        {
            //por questões de legibilidade ficará "persons"
            return _context.Persons.ToList();
        }
        public Person FindById(long id)
        {

            //logica para puxar do banco de dados
            return _context.Persons.SingleOrDefault(p => p.Id == id);

        }

        public Person Create(Person person)
        {
            try
            {
                _context.Add(person);
                _context.SaveChanges();

            } catch (Exception e)
            {
                throw new Exception("Error" + e.Message);
            }
            return person;
        }
        public Person Update(Person person)
        {
            bool exists = _context.Persons.Any(p => p.Id == person.Id);
            if (!exists) return new Person();
            //quando o id do banco de dados for igual o id do obj vamos buscar na base e armazenar em var
            var result = _context.Persons.SingleOrDefault(p => p.Id == person.Id);
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(person);
                    _context.SaveChanges();

                }
                catch (Exception e)
                {
                    throw new Exception("Error" + e.Message);
                }
            }
            return person;
        }
        public void Delete(long id)
        {
            var result = _context.Persons.SingleOrDefault(p => p.Id == id);
            if (result != null)
            {
                try
                {
                    //remove por Id mas poderiamos simplesmente remover passando person
                    _context.Persons.Remove(result);
                    _context.SaveChanges();

                }
                catch (Exception e)
                {
                    throw new Exception("Error" + e.Message);
                }
            }
        }
        public bool Exists(long id)
        {
            return _context.Persons.Any(p => p.Id.Equals(id));
        }
        
    }
}
