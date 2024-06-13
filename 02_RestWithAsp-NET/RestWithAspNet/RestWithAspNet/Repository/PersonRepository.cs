using RestWithAspNet.Repository.Generic;
using RestWithAspNet.Model;
using RestWithAspNet.Data;
using RestWithAspNet.Repository;

namespace RestWithAspNet.Repository
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(MysqlContext context) : base(context) 
        {

        }
        public Person Disable(int id)
        {
            if (_context.Persons.Any(p => p.Id.Equals(id))) return null;
            var user = _context.Persons.FirstOrDefault(p => p.Id.Equals(id));
            if(user != null)
            {
                //user.Enabled = false;
            }
            try
            {
                _context.Entry(user).CurrentValues.SetValues(user);
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
            return user;
        }
    }
}
