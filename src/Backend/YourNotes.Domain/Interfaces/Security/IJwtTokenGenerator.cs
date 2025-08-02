namespace YourNotes.Domain.Interfaces.Security
{
    public interface IJwtTokenGenerator
    {
        public string GenerationToken(Guid id);
    }
}
