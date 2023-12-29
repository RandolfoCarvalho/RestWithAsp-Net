using RestWithAspNet.Model;

namespace RestWithAspNet.Services
{
    public interface IPersonService
    {
        Person Create(Person person);
        Person Update(Person person);
        Person FindById(long id);
        List<Person> FindAll();
        void Delete(long id);
    }
}
