using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using YourNotes.Domain.Entities;
using YourNotes.Domain.Interfaces.Repositories.User;
using YourNotes.Domain.Interfaces.Services;
using YourNotes.Persistence.Autentication.Tokens.Access;

namespace YourNotes.Persistence.Repositories
{
    public class LoggedUser : ILoggedUser
    {
        private readonly ITokenValue _tokenValue;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;

        public LoggedUser(ITokenValue tokenValue, IUserReadOnlyRepository userReadOnlyRepository)
        {
            _tokenValue = tokenValue;
            _userReadOnlyRepository = userReadOnlyRepository;
        }
        public async Task<User> User()
        {
            var token = _tokenValue.Value();

            var jwtHandler = new JwtSecurityTokenHandler();

            var claims = jwtHandler.ReadJwtToken(token).Claims;

            var userIdentifier = claims.First(x => x.Type == ClaimTypes.Sid).Value;

            var userIdentifierAsGuid = new Guid(userIdentifier);

            var user = await _userReadOnlyRepository
                .GetAsync(userIdentifierAsGuid);

            return user!;
        }
    }
}
