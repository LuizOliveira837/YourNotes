using Microsoft.AspNetCore.Mvc;
using YourNotes.API.Filters;

namespace YourNotes.API.Attributes
{
    public class AuthenticatedUserAttribute : TypeFilterAttribute
    {
        public AuthenticatedUserAttribute() : base(typeof(AuthorizationFilter))
        {
        }
    }
}
