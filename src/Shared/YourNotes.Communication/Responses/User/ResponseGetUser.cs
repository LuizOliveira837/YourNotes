namespace YourNotes.Communication.Responses.User
{
    public class ResponseGetUser(string userName, string firstName, string lastName, string email, bool active)
    {
        public string UserName { get; set; } = userName;
        public string FirstName { get; set; } = firstName;
        public string LastName { get; set; } = lastName;
        public string Email { get; set; } = email;
        public bool Active { get; set; } = active;

    }
}
