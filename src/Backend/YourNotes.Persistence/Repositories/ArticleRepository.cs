using Microsoft.EntityFrameworkCore;
using YourNotes.Domain.Entities;
using YourNotes.Domain.Interfaces.Repositories.Article;
using YourNotes.Persistence.Data;

namespace YourNotes.Persistence.Repositories
{
    public class ArticleRepository : IArticleWriteOnlyRepository, IArticleReadOnlyRepository
    {
        private readonly YourNotesDbContext _context;

        public ArticleRepository(YourNotesDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ArticleExists(Guid articleId, Guid userId)
        {

            var query = from topic in _context.Topics
                        join article in _context.Articles on topic.Id equals article.TopicId
                        where (topic.UserId == userId && article.Id == articleId)
                        select topic;
            return
                await query
                .AsNoTracking()
                .AnyAsync();
        }

        public async Task CreateArticleContentAsync(Content content)
        {
              await _context.Contents.AddAsync(content);
        }

        public async Task CreateAsync(Article article) => await _context.Articles.AddAsync(article);
    }
}
