using RestWithAspNet.Configurations;
using RestWithAspNet.Data.VO;
using RestWithAspNet.Repository;
using RestWithAspNet.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace RestWithAspNet.Business.Implementations
{
    public class LoginBusinessImplementation : ILoginBusiness
    {
        private const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";
        private TokenConfiguration _configuration;
        private IUserRepository _repository;
        private readonly ITokenService _tokenService;

        public LoginBusinessImplementation(TokenConfiguration configuratin, IUserRepository repository, ITokenService tokenService)
        {
            _configuration = configuratin;
            _tokenService = tokenService;
            _repository = repository;
        }

        public TokenVO ValidateCredentials(UserVO userCredentials)
        {
            var user = _repository.ValidateCredentials(userCredentials);
            if (user == null) return null;
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
            };
            var accessToken = _tokenService.GenerateAcessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(_configuration.DaysToExpire);

            _repository.RefreshUserInfo(user);
            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate.AddMinutes(_configuration.Minutes);
            return new TokenVO
                (
                    true,
                    createDate.ToString(DATE_FORMAT),
                    expirationDate.ToString(DATE_FORMAT),
                    accessToken,
                    refreshToken
                );
        }

        public TokenVO ValidateCredentials(TokenVO token)
        {
            var accessToken = token.AccessToken;
            var refreshToken = token.RefreshToken;

            var principal =_tokenService.GetPrincipalFromExpiredToken(accessToken);

            var userName = principal.Identity.Name;

            var user = _repository.ValidateCredentials(userName);

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now) 
            {
                return null;
            }

            accessToken = _tokenService.GenerateAcessToken(principal.Claims);
            refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;

            _repository.RefreshUserInfo(user);
            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate.AddMinutes(_configuration.Minutes);
            return new TokenVO
                (
                    true,
                    createDate.ToString(DATE_FORMAT),
                    expirationDate.ToString(DATE_FORMAT),
                    accessToken,
                    refreshToken
                );
        }

        public bool RevokeToken(string userName)
        {
            return _repository.RevokeToken(userName);
        }
    }
}
