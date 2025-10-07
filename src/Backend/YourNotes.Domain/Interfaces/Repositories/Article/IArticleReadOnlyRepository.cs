namespace YourNotes.Domain.Interfaces.Repositories.Article
{
    public interface IArticleReadOnlyRepository
    {
        public Task<bool> ArticleExists(Guid articleId, Guid userId);
    }
}
