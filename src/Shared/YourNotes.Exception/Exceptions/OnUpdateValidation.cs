using System.Net;

namespace YourNotes.Exception.Exceptions
{
    public class OnUpdateValidation : YourNotesBaseException
    {
        public OnUpdateValidation(string error)
            : base(error, HttpStatusCode.BadRequest) { }

    }
}
