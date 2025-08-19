namespace YourNotes.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository<YourNotes.Domain.Entities.User>
    {

        public Task<bool> UserNameExistsAsync(string userName);
        public Task<bool> EmailExistsAsync(string email);
        public Task<bool> UserExistsAsync(Guid id);
        public Task<YourNotes.Domain.Entities.User?> UserExistsByEmailAndPassword(string email, string password);
    }
}
