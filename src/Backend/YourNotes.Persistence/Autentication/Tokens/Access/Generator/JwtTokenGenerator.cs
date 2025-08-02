using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using YourNotes.Domain.Interfaces.Security;

namespace YourNotes.Persistence.Autentication.Tokens.Access.Generator
{
    public class JwtTokenGenerator : JwtTokenHandler, IJwtTokenGenerator
    {
        private readonly string _secretKey;
        private readonly int _expirationTimeInMinutes;

        public JwtTokenGenerator(string secretKey, int expirationTimeInMinutes)
        {
            _secretKey = secretKey;
            _expirationTimeInMinutes = expirationTimeInMinutes;
        }
        public string GenerationToken(Guid id)
        {

            var tokenHandler = new JwtSecurityTokenHandler();


            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                    new(ClaimTypes.Sid, id.ToString()),
                ]),
                Expires = DateTime.UtcNow.AddMinutes(_expirationTimeInMinutes),
                SigningCredentials = new SigningCredentials(SecurityKey(_secretKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var securityToken = tokenHandler.CreateToken(descriptor);

            return tokenHandler.WriteToken(securityToken);

        }
    }
}
