using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using YourNotes.Domain.Entities;
using YourNotes.Domain.Interfaces.Services;
using YourNotes.Persistence.Autentication.Tokens.Access;
using YourNotes.Persistence.Data;

namespace YourNotes.Persistence.Autentication
{
    public class LoggedUser : ILoggedUser
    {
        private readonly ITokenValue _tokenValue;
        private readonly YourNotesDbContext _context;

        public LoggedUser(ITokenValue tokenValue, YourNotesDbContext context)
        {
            _tokenValue = tokenValue;
            _context = context;
        }
        public async Task<User> User()
        {
            var token = _tokenValue.Value();

            var jwtHandler = new JwtSecurityTokenHandler();

            var claims = jwtHandler.ReadJwtToken(token).Claims;

            var userIdentifier = claims.First(x => x.Type == ClaimTypes.Sid).Value;

            var userIdentifierAsGuid = new Guid(userIdentifier);

            return await _context
                .Users
                .FirstAsync(x => x.Id == userIdentifierAsGuid);
        }
    }
}
