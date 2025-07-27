namespace YourNotes.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        public Task Commit();
    }
}
