using Microsoft.EntityFrameworkCore;
using YourNotes.Domain.Entities;
using YourNotes.Domain.Interfaces.Repositories.User;
using YourNotes.Persistence.Data;

namespace YourNotes.Persistence.Repositories
{
    public class UserRepository : IUserReadOnlyRepository, IUserWriteOnlyRepository
    {
        private readonly YourNotesDbContext _context;

        public UserRepository(YourNotesDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> CreateAsync(User t)
        {
            await _context
                 .Users.AddAsync(t);

            return t.Id;


        }

        public async Task<bool> EmailExistsAsync(string email) => await _context.Users.AsNoTracking().AnyAsync(u => u.Email == email);

        public async Task<User?> GetAsync(Guid id) => await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<bool> UserExistsAsync(Guid id) => await _context.Users.AnyAsync(x => x.Id == id);

        public async Task<User?> UserExistsByEmailAndPassword(string email, string password)
        {
            return await _context
                 .Users
                 .AsNoTracking()
                 .FirstOrDefaultAsync(x => x.Email == email && x.Password == password && x.Active);

        }

        public async Task<bool> UserNameExistsAsync(string userName) => await _context.Users.AsNoTracking().AnyAsync(x => x.UserName == userName);
    }
}
