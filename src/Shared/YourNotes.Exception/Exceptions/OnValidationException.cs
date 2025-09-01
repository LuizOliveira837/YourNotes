using System.Net;

namespace YourNotes.Exception.Exceptions
{
    public class OnValidationException : YourNotesBaseException
    {
        public OnValidationException(string error)
            : base(error, HttpStatusCode.BadRequest)
        {

        }

        public OnValidationException(string error, HttpStatusCode statusCode)
            : base(error, statusCode)
        {

        }


    }
}
