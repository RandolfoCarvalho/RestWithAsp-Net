using System.Security.Claims;

namespace RestWithAspNet.Services
{
    public interface ITokenInterface
    {
        string GenerateAcessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
