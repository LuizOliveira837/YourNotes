using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace YourNotes.Persistence.Autentication.Tokens.Access
{
    public abstract class JwtTokenHandler
    {
        protected static SymmetricSecurityKey SecurityKey(string signingKey)
        {
            var signingKeyAsBytes = Encoding.UTF8.GetBytes(signingKey);


            return new SymmetricSecurityKey(signingKeyAsBytes);
        }

    }
}
