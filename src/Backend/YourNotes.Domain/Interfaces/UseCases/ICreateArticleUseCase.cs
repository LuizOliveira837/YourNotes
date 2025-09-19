using YourNotes.Communication.Requests.Article;

namespace YourNotes.Domain.Interfaces.UseCases
{
    public interface ICreateArticleUseCase
    {
        public Task Execute(RequestArticleJson request);
        public Task Validate(RequestArticleJson request, Guid userId);
    }
}
