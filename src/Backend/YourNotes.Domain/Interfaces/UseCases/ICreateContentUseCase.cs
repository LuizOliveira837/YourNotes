using YourNotes.Communication.Requests.Article;

namespace YourNotes.Domain.Interfaces.UseCases
{
    public interface ICreateContentUseCase
    {
        public Task Execute(RequestContentJson request);
        public Task Validate(RequestContentJson request, Guid userId);
    }
}
