using RestWithAspNet.Data.VO;
using RestWithAspNet.Data.Converter.Implementations;
using RestWithAspNet.Model;
using RestWithAspNet.Repository.Generic;
using RestWithAspNet.Repository;


namespace RestWithAspNet.Business.Implementations
{
    public class PersonBusinessImplementation : IPersonBusiness
    {

        private readonly IPersonRepository _repository;
        private readonly PersonConverter _converter;

        public PersonBusinessImplementation(IPersonRepository repository) 
        {
            _repository = repository;
            _converter = new PersonConverter();
        }

        public List<PersonVO> FindAll()
        {
            //por questões de legibilidade ficará "PersonVOs"
            return _converter.Parse(_repository.FindAll());
        }
        public PersonVO FindById(long id)
        {

            //logica para puxar do banco de dados
            return _converter.Parse( _repository.FindById(id));

        }

        public PersonVO Create(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Create(personEntity);
            return _converter.Parse(personEntity);
        }
        public PersonVO Update(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Update(personEntity);
            return _converter.Parse(personEntity);

        }
        public PersonVO Disable(int id)
        {
            var personEntity = _repository.Disable(id);
            return _converter.Parse(personEntity);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }
        
    }
}
