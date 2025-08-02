using System.IdentityModel.Tokens.Jwt;
using YourNotes.Persistence.Autentication.Tokens.Access;

namespace YourNotes.API.Token
{
    public class TokenJwtValue : ITokenValue
    {
        private readonly IHttpContextAccessor _context;

        public TokenJwtValue(IHttpContextAccessor context)
        {
            _context = context;
        }
        public string Value()
        {
            var authentication = _context.HttpContext!.Request.Headers.Authorization.ToString();

            return authentication["Bearer ".Length..].Trim();

        }
    }
}
