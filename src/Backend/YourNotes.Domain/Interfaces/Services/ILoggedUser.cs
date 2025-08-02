using YourNotes.Domain.Entities;

namespace YourNotes.Domain.Interfaces.Services
{
    public interface ILoggedUser
    {
        public Task<User> User();
    }
}
