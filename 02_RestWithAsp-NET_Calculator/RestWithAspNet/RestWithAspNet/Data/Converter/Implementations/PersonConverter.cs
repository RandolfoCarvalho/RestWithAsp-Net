using RestWithAspNet.Data.Converter.Contract;
using RestWithAspNet.Data.VO;
using RestWithAspNet.Model;

namespace RestWithAspNet.Data.Converter.Implementations
{
    public class PersonConverter : IParser<PersonVO, Person>, IParser<Person, PersonVO>
    {
        public Person Parse(PersonVO origem)
        {
            if (origem == null) return null;
            return new Person
            {
                Id = origem.Id,
                FirstName = origem.FirstName,
                LastName = origem.LastName,
                Adress = origem.Adress,
                Gender = origem.Gender
            };
        }

        public PersonVO Parse(Person origem)
        {
            if (origem == null) return null;
            return new PersonVO
            {
                Id = origem.Id,
                FirstName = origem.FirstName,
                LastName = origem.LastName,
                Adress = origem.Adress,
                Gender = origem.Gender
            };
        }

        public List<Person> Parse(List<PersonVO> origem)
        {
            if (origem == null) return null;
            return origem.Select(item => Parse(item)).ToList();
        }

        public List<PersonVO> Parse(List<Person> origem)
        {
            if (origem == null) return null;
            return origem.Select(item => Parse(item)).ToList();
        }
    }
}
