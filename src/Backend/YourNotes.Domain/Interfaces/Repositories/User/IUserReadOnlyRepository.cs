namespace YourNotes.Domain.Interfaces.Repositories.User
{
    public interface IUserReadOnlyRepository
    {
        public Task<bool> EmailExistsAsync(string email);
        public Task<YourNotes.Domain.Entities.User?> GetAsync(Guid id);
        public Task<bool> UserExistsAsync(Guid id);
        public Task<YourNotes.Domain.Entities.User?> UserExistsByEmailAndPassword(string email, string password);
        public Task<bool> UserNameExistsAsync(string userName);

    }
}
