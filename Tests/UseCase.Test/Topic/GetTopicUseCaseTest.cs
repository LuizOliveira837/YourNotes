using CommonTestUtilities.Builders;
using FluentAssertions;
using YourNotes.Application.Topic.GetTopic;
using YourNotes.Domain.Interfaces.Repositories.Topic;

namespace UseCases.Test.Topic
{
    public class GetTopicUseCaseTest
    {

        public GetTopicUseCase CreateUseCase(YourNotes.Domain.Entities.User? user, string? title = "")
        {
            var mapper = MapperBuilder.Build();
            var loggedUser = new LoggedUserBuilder().Build(user).loggedUser.Object;
            var readRepository = new TopicReadOnlyRepositoryBuilder(title, user).repository.Object;


            return new GetTopicUseCase(mapper, loggedUser, readRepository);
        }

        [Fact]
        public async Task Sucess()
        {
            //arrange
            var title = "WORK";
            var user = UserBuilder.Build();
            var useCase = CreateUseCase(user, title);

            //act
            var result = await useCase.Execute();

            //assert
            result
                .Topics
                .Should()
                .HaveCount(1);

            result
               .Topics
               .Any(t => t.Title == title)
               .Should()
               .BeTrue();
        }

        [Fact]
        public async Task Sucess_EMPTY_LIST()
        {
            //arrange
            var user = UserBuilder.Build();
            var useCase = CreateUseCase(user);

            //act
            var result = await useCase.Execute();

            //assert
            result
                .Topics
                .Should()
                .HaveCount(0);

        }
    }
}
