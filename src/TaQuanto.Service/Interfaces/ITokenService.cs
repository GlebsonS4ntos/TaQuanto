using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace TaQuanto.Service.Interfaces
{
    public interface ITokenService
    {
        public string GenerateRefreshToken();
        public JwtSecurityToken GenerateToken(IEnumerable<Claim> claims);
        public ClaimsPrincipal GetClaimsPrincipalFromToken(string token);
    }
}
