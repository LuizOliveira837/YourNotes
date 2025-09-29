using CommonTestUtilities.Builders;
using FluentAssertions;
using YourNotes.Application.Article.CreateArticle;
using YourNotes.Communication.Requests.Article;
using YourNotes.Exception;
using YourNotes.Exception.Exceptions;

namespace UseCases.Test.Article
{
    public class CreateArticleUseCaseTest
    {

        public CreateArticleUseCase CreateUseCase(YourNotes.Domain.Entities.User user, YourNotes.Domain.Entities.Topic? topic = null)
        {
            var loggedUser = new LoggedUserBuilder().Build(user).loggedUser.Object;
            var topicReadRepository = new TopicReadOnlyRepositoryBuilder(user).TopicExists(user.Id, topic).repository.Object;
            var mapper = MapperBuilder.Build();
            var uof = new UnitOfWorkBuilder().uof.Object;
            var articleWriteRepository = new ArticleWriteOnlyRepositoryBuilder().repository.Object;
            return new CreateArticleUseCase(loggedUser, topicReadRepository, mapper, uof, articleWriteRepository);
        }

        [Fact]
        public async Task Sucess()
        {
            //arrange
            var user = UserBuilder.Build();
            var topic = new YourNotes.Domain.Entities.Topic(user.Id, "Teste", "Breve descrição teste");
            var article = ArticleBuilder.Build(topic.Id);
            var request = new RequestArticleJson()
            {
                Description = article.Description,
                Title = article.Title,
                TopicId = article.TopicId,
            };

            var useCase = CreateUseCase(user, topic);

            //act

            await useCase.Execute(request);

            //assert
        }

        [Fact]
        public async Task ERROR_INVALID_TITLE()
        {
            //arrange
            var user = UserBuilder.Build();
            var topic = new YourNotes.Domain.Entities.Topic(user.Id, "Teste", "Breve descrição teste");
            var article = ArticleBuilder.Build(topic.Id);
            var request = new RequestArticleJson()
            {
                Description = article.Description,
                Title = string.Empty,
                TopicId = article.TopicId,
            };

            var useCase = CreateUseCase(user, topic);

            //act

            var result = async () => await useCase.Execute(request);

            //assert

            await (result)
                .Should()
                .ThrowAsync<OnValidationException>()
                .Where(e => e.Message.ToString() == YourNotesExceptionResource.INVALID_ARTICLE_LENGTH);
        }
    }
}
