using System.Net;

namespace YourNotes.Exception.Exceptions
{
    public class OnAuthorizationException : YourNotesBaseException
    {
        public OnAuthorizationException(string error)
            :base(error, HttpStatusCode.Unauthorized)
        {
            
        }
    }
}
