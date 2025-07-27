using YourNotes.Communication.Requests.User;
using YourNotes.Communication.Responses.User;

namespace YourNotes.Domain.Interfaces.UseCases
{
    public interface IUpdateUserNameUseCase
    {
        public Task<ResponseUpdateUserName> Execute(Guid id, RequestUpdateUserName request);
        public Task Validate(RequestUpdateUserName request);
    }
}
