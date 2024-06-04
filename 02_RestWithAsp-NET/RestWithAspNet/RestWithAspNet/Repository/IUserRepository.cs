using RestWithAspNet.Data.VO;
using RestWithAspNet.Model;

namespace RestWithAspNet.Repository
{
    public interface IUserRepository
    {
        User ValidateCredentials(UserVO user);
        User ValidateCredentials(string userName);

        bool RevokeToken(string userName);
        User RefreshUserInfo(User info);
    }
}
