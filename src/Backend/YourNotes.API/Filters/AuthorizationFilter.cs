using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using YourNotes.Communication.Responses;
using YourNotes.Domain.Interfaces.Repositories;
using YourNotes.Domain.Interfaces.Repositories.User;
using YourNotes.Domain.Interfaces.Security;
using YourNotes.Exception;
using YourNotes.Exception.Exceptions;

namespace YourNotes.API.Filters
{
    public class AuthorizationFilter(IJwtTokenValidator validatorToken, IUserReadOnlyRepository userRepository) : IAsyncAuthorizationFilter
    {
        private readonly IJwtTokenValidator _validatorToken = validatorToken;
        private readonly IUserReadOnlyRepository _userRepository = userRepository;

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            try
            {
                var token = GetToken(context);

                var userIdentifier = _validatorToken.ValidateTokenAndGetUserIdentifier(token);

                if (userIdentifier.Equals(Guid.Empty)) throw new OnAuthorizationException(YourNotesExceptionResource.USER_WITHOUT_AUTHORIZATION);

                if (!await _userRepository.UserExistsAsync(userIdentifier)) throw new OnAuthorizationException(YourNotesExceptionResource.USER_NOT_FOUND);

            }
            catch (OnAuthorizationException ex)
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(ex.Error));
            }
            catch
            {
                context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(YourNotesExceptionResource.USER_WITHOUT_AUTHORIZATION));

            }

        }

        public string GetToken(AuthorizationFilterContext context)
        {
            var autorization = context.HttpContext.Request.Headers.Authorization.ToString();

            if (string.IsNullOrEmpty(autorization)) throw new OnAuthorizationException(YourNotesExceptionResource.USER_WITHOUT_AUTHORIZATION);


            return autorization["Bearer ".Length..];
        }
    }
}
