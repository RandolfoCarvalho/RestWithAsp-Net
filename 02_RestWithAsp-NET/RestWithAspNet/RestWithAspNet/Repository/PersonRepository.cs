using RestWithAspNet.Repository.Generic;
using RestWithAspNet.Model;
using RestWithAspNet.Data;
using RestWithAspNet.Repository;

namespace RestWithAspNet.Repository
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(MysqlContext Context) : base(Context) 
        {

        }
        public Person Disable(int id)
        {
            var user = _context.Persons.FirstOrDefault(p => p.Id.Equals(id));
            if (user == null);
            user.Enabled = false;
            try
            {
                _context.Entry(user).CurrentValues.SetValues(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro" + ex.Message);
            }
            return user;
        }

        public List<Person> FindByName(string firstName, string secondName)
        {
            if(!string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(firstName))
            {
                return _context.Persons.Where(
                p => p.FirstName.Contains(firstName)
                && p.LastName.Contains(secondName)).ToList();
            } 
            else if (string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(firstName))
            {
                return _context.Persons.Where(
                p => p.FirstName.Contains(firstName)
                && p.LastName.Contains(secondName)).ToList();
            }
            else if (!string.IsNullOrWhiteSpace(firstName) && string.IsNullOrWhiteSpace(firstName))
            {
                return _context.Persons.Where(
                p => p.FirstName.Contains(firstName)
                && p.LastName.Contains(secondName)).ToList();
            }
            return null;
        }
    }
}
