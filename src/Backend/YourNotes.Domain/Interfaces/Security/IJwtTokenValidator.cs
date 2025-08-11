namespace YourNotes.Domain.Interfaces.Security
{
    public interface IJwtTokenValidator
    {

        public Guid ValidateTokenAndGetUserIdentifier(string token);
    }
}
