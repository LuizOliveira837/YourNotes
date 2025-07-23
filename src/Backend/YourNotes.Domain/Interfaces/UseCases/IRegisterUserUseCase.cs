using YourNotes.Communication.Request;
using YourNotes.Communication.Responses;

namespace YourNotes.Domain.Interfaces.UseCases
{
    public interface IRegisterUserUseCase
    {

        public Task<ResponseRegisterUser> Execute(RequestRegisterUser request);
        public Task Validate(RequestRegisterUser request);
    }
}
