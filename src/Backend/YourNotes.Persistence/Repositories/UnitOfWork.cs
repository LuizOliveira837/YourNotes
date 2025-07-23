using YourNotes.Domain.Interfaces.Repositories;
using YourNotes.Persistence.Data;

namespace YourNotes.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly YourNotesDbContext _context;
        public UnitOfWork(YourNotesDbContext context)
        {
            _context = context;
        }
        public async Task Commit() => await _context.SaveChangesAsync();
    }
}
