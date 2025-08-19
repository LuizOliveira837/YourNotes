namespace YourNotes.Domain.Interfaces.Repositories.User
{
    public interface IUserWriteOnlyRepository
    {
        public Task<Guid> CreateAsync(YourNotes.Domain.Entities.User t);
    }
}
