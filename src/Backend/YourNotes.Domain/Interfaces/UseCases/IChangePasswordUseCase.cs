using YourNotes.Communication.Requests.User;

namespace YourNotes.Domain.Interfaces.UseCases
{
    public interface IChangePasswordUseCase
    {
        public Task Execute(RequestChangePassword request);
        public void Validate(RequestChangePassword request, string currentPassword);
    }
}
