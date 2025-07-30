namespace YourNotes.Exception.Exceptions
{
    public class OnValidationException : YourNotesBaseException
    {
        public OnValidationException(string error)
            : base(error)
        {
        }


    }
}
