namespace YourNotes.Communication.Responses.User
{
    public class ResponseRegisterUser(Guid id, Token token)
    {
        public Guid Id { get; set; } = id;
        public Token Token { get; set; } = token;
    }
}
