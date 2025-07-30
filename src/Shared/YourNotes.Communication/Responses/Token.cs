namespace YourNotes.Communication.Responses
{
    public class Token(string accessToken)
    {
        public string AccessToken { get; set; } = accessToken;
    }
}
