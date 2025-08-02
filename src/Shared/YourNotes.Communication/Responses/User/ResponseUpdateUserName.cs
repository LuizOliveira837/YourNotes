namespace YourNotes.Communication.Responses.User
{
    public class ResponseUpdateUserName(string username)
    {
        public string Username { get; set; } = username;
    }
}
