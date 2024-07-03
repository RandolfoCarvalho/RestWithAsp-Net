using RestWithAspNet.Data.VO;
using RestWithAspNet.Model;

namespace RestWithAspNet.Business
{
    public interface IPersonBusiness
    {
        PersonVO Create(PersonVO person);
        PersonVO Update(PersonVO person);
        PersonVO FindById(long id);

        List<PersonVO> FindByName(string firstName, string secondName);
        List<PersonVO> FindAll();
        PersonVO Disable(int id);
        void Delete(long id);
    }
}
