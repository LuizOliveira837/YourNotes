using Microsoft.EntityFrameworkCore;
using YourNotes.Domain.Entities;

namespace YourNotes.Persistence.Data
{
    public class YourNotesDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public YourNotesDbContext(DbContextOptions<YourNotesDbContext> options)
            : base(options)
        {

        }
    }
}
