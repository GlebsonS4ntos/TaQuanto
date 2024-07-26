using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TaQuanto.Service.Helpers;
using TaQuanto.Service.Interfaces;

namespace TaQuanto.Service.Services
{
    public class TokenService : ITokenService
    {

        private readonly JwtConfig _jwtConfig;

        public TokenService(JwtConfig jwtConfig)
        {
            _jwtConfig = jwtConfig;
        }

        public string GenerateRefreshToken()
        {
            var securityRandomBytes = new byte[128];
            
            using var randomGenerator = RandomNumberGenerator.Create();
            randomGenerator.GetBytes(securityRandomBytes);

            return Convert.ToBase64String(securityRandomBytes);
        }

        public JwtSecurityToken GenerateToken(IEnumerable<Claim> claims)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.SecretKey));
            var signinCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                SigningCredentials = signinCredentials,
                Expires = DateTime.UtcNow.AddMinutes(_jwtConfig.TokenValidityInMinutes),
                Subject = new ClaimsIdentity(claims),
                Issuer = _jwtConfig.ValidIssuer,
                Audience = _jwtConfig.ValidAudience
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            
            return tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
        }

        public ClaimsPrincipal GetClaimsPrincipalFromToken(string token)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.SecretKey));

            var validateTokenParameters = new TokenValidationParameters()
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(token))
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var claims = tokenHandler.ValidateToken(token, validateTokenParameters, out SecurityToken validateToken);

            if (validateToken is not JwtSecurityToken jwtSecurityToken || 
                jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase)) 
            {
                throw new SecurityTokenException();
            }

            return claims;
        }
    }
}
