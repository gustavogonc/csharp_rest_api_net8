using System.Security.Claims;

namespace RestWithASP_NET8Udemy.Services
{
    public interface ITokenService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToeken(string token);
    }
}
