namespace YourNotes.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : class
    {

        public Task<Guid> CreateAsync(T t);
        public Task<T?> GetAsync(Guid id);

    }
}
