using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using YourNotes.Domain.Security;

namespace YourNotes.Persistence.Autentication.Tokens.Access.Validator
{
    public class JwtTokenValidator : JwtTokenHandler, IJwtTokenValidator
    {
        private readonly string _signingKey;

        public JwtTokenValidator(string signingKey)
        {
            _signingKey = signingKey;
        }


        public Guid ValidateTokenAndGetUserIdentifier(string token)
        {

            var handler = new JwtSecurityTokenHandler();


            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = SecurityKey(_signingKey),
                ClockSkew = new TimeSpan(0)

            };


            var claims = handler.ValidateToken(token, parameters, out _);


            var userIdentifier = claims.Claims.First(x => x.Type == ClaimTypes.Sid).Value;


            return new Guid(userIdentifier);

        }



    }
}
