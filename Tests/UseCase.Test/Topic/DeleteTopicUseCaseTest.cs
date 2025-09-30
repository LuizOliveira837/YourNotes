using CommonTestUtilities.Builders;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using System.Net;
using YourNotes.Application.Topic.DeleteTopic;
using YourNotes.Domain.Entities;
using YourNotes.Domain.Interfaces.UseCases;
using YourNotes.Exception;
using YourNotes.Exception.Exceptions;

namespace UseCases.Test.Topic
{
    public class DeleteTopicUseCaseTest
    {

        public IDeleteTopicUseCase
            CreateUseCase(YourNotes.Domain.Entities.User user, YourNotes.Domain.Entities.Topic topic = null)
        {
            var loggedUser = new LoggedUserBuilder().Build(user).loggedUser.Object;
            var writeRepository = new TopicWriteOnlyRepositoryBuilder(topic)
                .GetTopicByIdAndUserId(user.Id, topic)
                .repository.Object;

            var uof = new UnitOfWorkBuilder().uof.Object;

            return new DeleteTopicUseCase(loggedUser, writeRepository, uof);
        }

        [Fact]
        public async Task Sucess()
        {
            //arrange
            var user = UserBuilder.Build();
            var topic = TopicBuilder.Build(user.Id);
            var useCase = CreateUseCase(user, topic);

            //act

            await useCase.Execute(topic.Id);

            //assert
        }

        [Fact]
        public async Task ERROR_TOPIC_NOT_FOUND()
        {
            //arrange
            var user = UserBuilder.Build();
            var topic = TopicBuilder.Build(user.Id);
            var useCase = CreateUseCase(user);

            //act

            Func<Task> result = async () => await useCase.Execute(topic.Id);

            //assert

            await result
                 .Should()
                 .ThrowAsync<OnValidationException>()
                 .Where(e => e.Error == YourNotesExceptionResource.TOPIC_NOT_EXISTS
                 && e.StatusCode == HttpStatusCode.NotFound);

            
        }
    }
}
