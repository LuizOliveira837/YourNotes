using Microsoft.EntityFrameworkCore;
using YourNotes.Domain.Entities;
using YourNotes.Domain.Interfaces.Repositories;
using YourNotes.Persistence.Data;

namespace YourNotes.Persistence.Repositories
{
    public class UserRepository : IBaseRepository<User>, IUserRespository
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

        public Guid DeleteAsync(User user)
        {
            _context
                    .Users.Remove(user);

            return user.Id;
        }

        public async Task<bool> EmailExistsAsync(string email) => await _context.Users.AnyAsync(u => u.Email == email);

        public async Task<User?> GetAsync(Guid id) => await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<bool> UserNameExistsAsync(string userName) => await _context.Users.AnyAsync(x => x.UserName == userName);
    }
}

