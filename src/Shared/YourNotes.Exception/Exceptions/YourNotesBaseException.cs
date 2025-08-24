using System.Net;

namespace YourNotes.Exception.Exceptions
{
    public class YourNotesBaseException : SystemException
    {
        public string Error { get; set; } = string.Empty;
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.InternalServerError;


        public YourNotesBaseException(string error)
            : base(error)
        {
            Error = error;
        }

        public YourNotesBaseException(string error, HttpStatusCode statusCode)
            : base(error)
        {
            Error = error;
            StatusCode = statusCode;
        }

        public string GetMessage() => Error;
        public int GetStatusCode() => (int) StatusCode;

    }
}
