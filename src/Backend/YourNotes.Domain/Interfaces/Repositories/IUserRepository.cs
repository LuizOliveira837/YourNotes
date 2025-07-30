using YourNotes.Domain.Entities;

namespace YourNotes.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {

        public Task<bool> UserNameExistsAsync(string userName);
        public Task<bool> EmailExistsAsync(string email);
    }
}
