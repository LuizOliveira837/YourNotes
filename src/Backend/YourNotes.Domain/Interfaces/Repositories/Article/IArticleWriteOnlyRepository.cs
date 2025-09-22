namespace YourNotes.Domain.Interfaces.Repositories.Article
{
    public interface IArticleWriteOnlyRepository
    {
        public Task CreateAsync(YourNotes.Domain.Entities.Article article);
    }
}
