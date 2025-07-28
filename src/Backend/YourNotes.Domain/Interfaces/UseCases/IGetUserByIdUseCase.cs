using YourNotes.Communication.Responses;

namespace YourNotes.Domain.Interfaces.UseCases
{
    public interface IGetUserByIdUseCase
    {
        public Task<ResponseGetUser> Execute(Guid Id);
        public void Validate(Guid Id);
    }
}
