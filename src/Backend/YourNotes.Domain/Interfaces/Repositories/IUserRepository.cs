using YourNotes.Domain.Entities;

namespace YourNotes.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {

        public Task<bool> UserNameExistsAsync(string userName);
        public Task<bool> EmailExistsAsync(string email);
        public Task<bool> UserExistsAsync(Guid id);
        public Task<User?> UserExistsByEmailAndPassword(String email, string password);
    }
}
