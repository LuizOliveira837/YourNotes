namespace YourNotes.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        public Task Commit();
    }
}
