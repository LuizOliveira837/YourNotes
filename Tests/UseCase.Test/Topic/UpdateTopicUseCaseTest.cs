using CommonTestUtilities.Builders;
using FluentAssertions;
using YourNotes.Application.Topic.UpdateTopic;
using YourNotes.Exception;

namespace UseCases.Test.Topic
{
    public class UpdateTopicUseCaseTest
    {
        public UpdateTopicUseCase CreateUseCase(YourNotes.Domain.Entities.User user, YourNotes.Domain.Entities.Topic topic)
        {
            var loggedUser = new LoggedUserBuilder().Build(user).loggedUser.Object;
            var writeRepository = new TopicWriteOnlyRepositoryBuilder(topic).GetTopicByIdAndUserId(topic.Id, topic.UserId, topic).repository.Object;
            var readRepository = new TopicReadOnlyRepositoryBuilder(user: user).GetTopicByIdAndUserId(topic.Id, topic.UserId, topic).repository.Object;
            var uof = new UnitOfWorkBuilder().uof.Object;

            return new UpdateTopicUseCase(writeRepository, readRepository, loggedUser, uof);
        }

        [Fact]
        public async Task Sucess()
        {
            //arrange
            var user = UserBuilder.Build();
            var topic = new YourNotes.Domain.Entities.Topic(user.Id, "Teste", "Breve descrição teste");
            var useCase = CreateUseCase(user, topic);
            var request = RequestUpdateTopicJsonBuilder.Build(topic.Id);
            

            //act
            var result = await useCase.Execute(request);


            //assert

            result
                .Id
                .Should()
                .Be(topic.Id);

            result
                .Title
                .Should()
                .Be(request.NewTitle);
        }

        [Fact]
        public void ERROR_TOPIC_NOT_FOUND()
        {
            //arrange
            var user = UserBuilder.Build();
            var topic = new YourNotes.Domain.Entities.Topic(user.Id, "Teste", "Breve descrição teste");
            var useCase = CreateUseCase(user, topic);
            var request = RequestUpdateTopicJsonBuilder.Build(Guid.NewGuid());


            //act
            var result = async ()=> await  useCase.Execute(request);


            //assert
            (result.Should().ThrowAsync())
                .WithMessage(YourNotesExceptionResource.TOPIC_NOT_FOUND);

        }
    }
}
