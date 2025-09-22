using YourNotes.Domain.Entities;
using YourNotes.Domain.Interfaces.Repositories.Article;
using YourNotes.Persistence.Data;

namespace YourNotes.Persistence.Repositories
{
    public class ArticleRepository : IArticleWriteOnlyRepository
    {
        private readonly YourNotesDbContext _context;

        public ArticleRepository(YourNotesDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(Article article) => await _context.Articles.AddAsync(article);
    }
}
