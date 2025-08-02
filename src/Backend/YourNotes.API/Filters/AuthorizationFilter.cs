using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using YourNotes.Communication.Responses;
using YourNotes.Domain.Interfaces.Security;
using YourNotes.Exception;
using YourNotes.Exception.Exceptions;

namespace YourNotes.API.Filters
{
    public class AuthorizationFilter : IAsyncAuthorizationFilter
    {
        private readonly IJwtTokenValidator _validatorToken;

        public AuthorizationFilter(IJwtTokenValidator validatorToken)
        {
            _validatorToken = validatorToken;
        }
        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            try
            {
                var token = GetToken(context);
                var userIdentifier = _validatorToken.ValidateTokenAndGetUserIdentifier(token);

                if (userIdentifier.Equals(Guid.Empty)) throw new OnAuthorizationException(YourNotesExceptionResource.USER_WITHOUT_AUTHORIZATION);

                context.HttpContext.Request.Headers.Append("userIdentifier", userIdentifier.ToString());

                return Task.CompletedTask;
            }
            catch (OnAuthorizationException ex)
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(ex.Error));
                return Task.CompletedTask;
            }
            catch
            {
                context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(YourNotesExceptionResource.USER_WITHOUT_AUTHORIZATION));
                return Task.CompletedTask;

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
