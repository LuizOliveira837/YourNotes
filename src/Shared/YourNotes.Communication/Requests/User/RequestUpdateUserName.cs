namespace YourNotes.Communication.Requests.User
{
    public class RequestUpdateUserName
    {
        public RequestUpdateUserName(string userName)
        {
            UserName = userName;
        }

        public string UserName { get; set; } = string.Empty;
    }
}
