using CommonTestUtilities.Builders;
using FluentAssertions;
using YourNotes.Application.Topic.CreateTopic;
using YourNotes.Communication.Requests.Topic;
using YourNotes.Exception;
using YourNotes.Exception.Exceptions;

namespace UseCases.Test.Topic
{
    public class CreateTopicUseCaseTest
    {
        public YourNotes.Domain.Entities.Topic topic;
        public CreateTopicUseCase Build(YourNotes.Domain.Entities.User user, string title = "")
        {
            topic = new YourNotes.Domain.Entities.Topic(user.Id, title);
            var readRepository = new TopicReadOnlyRepositoryBuilder(user, title).repository.Object;
            var writeRepository = new TopicWriteOnlyRepositoryBuilder(topic).repository.Object;
            var uof = new UnitOfWorkBuilder().uof.Object;
            var loggedUser = new LoggedUserBuilder().Build(user).loggedUser.Object;
            var mapper = MapperBuilder.Build();
            return new CreateTopicUseCase(uof, loggedUser, mapper, readRepository, writeRepository);
        }

        [Fact]
        public async Task Success()
        {
            //arrange
            var user = UserBuilder.Build();
            var request = RequestTopicJsonBuilder.Build();
            var useCase = Build(user);

            //act
            var result = await useCase.Execute(request);

            //assert
            result
                .Title
                .Should()
                .Be(request.Title);

            result
                .Id
                .Should()
                .Be(topic.Id);
        }

        [Fact]
        public async Task ERROR_TITLE_ALREADY_EXISTS()
        {
            //arrange
            var user = UserBuilder.Build();
            var request = RequestTopicJsonBuilder.Build();
            var useCase = Build(user, request.Title);

            //act
            var result = async () => await useCase.Execute(request);

            //assert

            await (result)
                .Should().ThrowAsync<OnValidationException>()
                .Where(e => e.Error == YourNotesExceptionResource.TITLE_ALREADY_EXISTS);
                

        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public async Task ERROR_INVALID_TITLE(string title)
        {
            //arrange
            var user = UserBuilder.Build();
            var request = new RequestTopicJson
            {
                Title = title,
            };

            var useCase = Build(user, title);

            //act
            var result = async () => await useCase.Execute(request);

            //assert

            await (result)
                .Should().ThrowAsync<OnValidationException>()
                .Where(e => e.Error == YourNotesExceptionResource.INVALID_TITLE);


        }

    }
}
