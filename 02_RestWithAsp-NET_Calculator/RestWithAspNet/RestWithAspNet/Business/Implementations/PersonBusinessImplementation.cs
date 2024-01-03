using RestWithAspNet.Data;
using RestWithAspNet.Model;
using RestWithAspNet.Repository;
using System;
using System.Security.Cryptography;

namespace RestWithAspNet.Business.Implementations
{
    public class PersonBusinessImplementation : IPersonBusiness
    {

        private readonly IPersonRepository _repository;

        public PersonBusinessImplementation(IPersonRepository repository) 
        {
            _repository = repository;
        }

        public List<Person> FindAll()
        {
            //por questões de legibilidade ficará "persons"
            return _repository.FindAll();
        }
        public Person FindById(long id)
        {

            //logica para puxar do banco de dados
            return _repository.FindById(id);

        }

        public Person Create(Person person)
        {
            return _repository.Create(person);
        }
        public Person Update(Person person)
        {
            return _repository.Update(person);
           
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }
        
    }
}
