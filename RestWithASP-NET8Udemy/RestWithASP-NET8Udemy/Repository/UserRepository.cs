using RestWithASP_NET8Udemy.Data.VO;
using RestWithASP_NET8Udemy.Model;
using RestWithASPNETErudio.Model.Context;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace RestWithASP_NET8Udemy.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MySQLContext _context;
        public UserRepository(MySQLContext context)
        {
            _context = context;
        }
        public User ValidateCredentials(string userName)
        {
            return _context.Users.SingleOrDefault(u => u.UserName == userName);
        }
        public User ValidateCredentials(UserVO user)
        {
            var pass = ComputerHash(user.Password, SHA256.Create());
            return _context.Users.FirstOrDefault(u => (u.UserName == user.Username) && ( u.Password == pass));
        }
        public bool RevokeToken(string userName)
        {
            var user = _context.Users.SingleOrDefault(u => u.UserName == userName);
            if(user is null)
            {
                return false;
            }
            user.RefreshToken = null;
            _context.SaveChanges();

            return true;
        }

        public User RefreshUserInfo(User user)
        {
            if(!_context.Users.Any(p => p.Id.Equals(user.Id)))
            {
                return null;
            }

            var result = _context.Users.SingleOrDefault(p => p.Id.Equals(user.Id));
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
                return result;
            
        }

        private object ComputerHash(string input, SHA256 algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);
            return BitConverter.ToString(hashedBytes);
        }

    }
}
