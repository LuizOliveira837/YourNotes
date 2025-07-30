using YourNotes.Communication.Requests.User;
using YourNotes.Communication.Responses.User;

namespace YourNotes.Domain.Interfaces.UseCases
{
    public interface IRegisterUserUseCase
    {

        public Task<ResponseRegisterUser> Execute(RequestRegisterUser request);
        public Task Validate(RequestRegisterUser request);
    }
}
