using Moq;
using YourNotes.Domain.Interfaces.Repositories.Article;

namespace CommonTestUtilities.Builders
{
    public class ArticleWriteOnlyRepositoryBuilder
    {

        public Mock<IArticleWriteOnlyRepository> repository;

        public ArticleWriteOnlyRepositoryBuilder()
        {
            repository = new();
        }
    }
}
