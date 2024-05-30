using RestWithAspNet.Data;
using RestWithAspNet.Data.VO;
using RestWithAspNet.Model;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace RestWithAspNet.Repository
{
    public class UserRepository :IUserRepository
    {
        private readonly MysqlContext _context;

        public UserRepository(MysqlContext context)
        {
            _context = context;
        }

        public User ValidateCredentials(UserVO user)
        {
            var pass = ComputeHash(user.Password, new SHA256CryptoServiceProvider());
            return _context.Users.FirstOrDefault(u => (u.UserName == user.UserName) && (u.Password == pass));
        }

        public User RefreshUserInfo(User user)
        {
            if (!_context.Users.Any(u => u.Id.Equals(user.Id))) return null;
            var result = _context.Users.SingleOrDefault(p => p.Id.Equals(user.Id));
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                    return result;
                }
                catch (Exception e)
                {
                    throw;
                }
            }
            return result;
        }

        public User ValidateCredentials(string userName)
        {
            return _context.Users.SingleOrDefault(u => u.UserName == userName);
        }

        public bool Exists(long id)
        {
            return _context.Users.Any(p => p.Id.Equals(id));
        }
        //encriptção de senha
        private string ComputeHash(string input, SHA256CryptoServiceProvider algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);
            return BitConverter.ToString(hashedBytes);
        }

        
    }
}
