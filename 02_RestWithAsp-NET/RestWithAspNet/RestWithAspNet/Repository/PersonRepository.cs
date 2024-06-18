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
            if (user == null) throw new Exception("O erro foi em PersonRepository bro");
            user.Enabled = false;
            try
            {
                _context.Entry(user).CurrentValues.SetValues(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("nao deu bom aqui" + ex.Message);
            }
            return user;
        }
    }
}
