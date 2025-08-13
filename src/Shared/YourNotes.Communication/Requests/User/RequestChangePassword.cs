namespace YourNotes.Communication.Requests.User
{
    public class RequestChangePassword
    {
        public string Password { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}
