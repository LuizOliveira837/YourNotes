namespace YourNotes.Communication.Responses
{
    public class ResponseErrorJson
    {
        public List<string> Errors {  get; set; }

        public ResponseErrorJson(string error)
        {
            Errors = new List<string> { error };
        }


        public ResponseErrorJson(List<string> errors)
        {
            Errors = errors;
        }

    }
}
