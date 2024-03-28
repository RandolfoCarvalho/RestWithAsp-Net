using RestWithAspNet.Data.VO;

namespace RestWithAspNet.Business
{
    public interface ILoginBusiness
    {
        TokenVO ValidateCredentials(UserVO user);
    }
}
