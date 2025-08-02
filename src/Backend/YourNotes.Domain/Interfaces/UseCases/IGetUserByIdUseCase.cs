using YourNotes.Communication.Responses.User;

namespace YourNotes.Domain.Interfaces.UseCases
{
    public interface IGetUserByIdUseCase
    {
        public Task<ResponseGetUser> Execute();
        public void Validate(Guid Id);
    }
}
