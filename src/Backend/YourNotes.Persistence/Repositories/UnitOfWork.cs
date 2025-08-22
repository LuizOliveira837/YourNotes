using YourNotes.Domain.Interfaces.Repositories;
using YourNotes.Persistence.Data;

namespace YourNotes.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly YourNotesDbContext _context;
        public UnitOfWork(YourNotesDbContext context)
        {
            _context = context;
        }
        public async Task Commit() => await _context.SaveChangesAsync();

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
