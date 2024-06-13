using RestWithAspNet.Data.VO;
using RestWithAspNet.Model;
using RestWithAspNet.Repository.Generic;

namespace RestWithAspNet.Repository
{
    public interface IPersonRepository : IRepository<Person>
    {
        Person Disable(int id);
    }
}
