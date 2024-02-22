using RestWithASP_NET8Udemy.Data.VO;
using RestWithASP_NET8Udemy.Model;

namespace RestWithASP_NET8Udemy.Repository
{
    public interface IUserRepository
    {
        User ValidateCredentials(UserVO user);
    }
}
