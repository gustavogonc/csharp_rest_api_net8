using RestWithASP_NET8Udemy.Data.VO;

namespace RestWithASP_NET8Udemy.Business
{
    public interface ILoginBusiness
    {
        TokenVO ValidateCredentials(UserVO user);
    }
}
