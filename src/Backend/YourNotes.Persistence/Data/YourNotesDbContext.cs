using Microsoft.EntityFrameworkCore;
using YourNotes.Domain.Entities;

namespace YourNotes.Persistence.Data
{
    public class YourNotesDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Article> Articles { get; set; }
        public YourNotesDbContext(DbContextOptions<YourNotesDbContext> options)
            : base(options)
        {

        }
    }
}
