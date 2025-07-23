namespace YourNotes.Domain.Interfaces.Repositories
{
    public interface IUserRespository
    {

        public Task<bool> UserNameExistsAsync(string userName);
        public Task<bool> EmailExistsAsync(string email);
    }
}
