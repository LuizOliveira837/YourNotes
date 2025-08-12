using YourNotes.Communication.Requests.Login;
using YourNotes.Communication.Responses.User;

namespace YourNotes.Domain.Interfaces.UseCases
{
    public interface ILoginByEmailAndPasswordUseCase
    {
        public Task<ResponseRegisterUser> Execute(RequestLoginByEmailAndPassword request);

        public Task<YourNotes.Domain.Entities.User> ValidateAndReturnUser(RequestLoginByEmailAndPassword request);
    }
}
