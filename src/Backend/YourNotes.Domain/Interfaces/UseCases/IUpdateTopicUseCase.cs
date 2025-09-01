using YourNotes.Communication.Requests.Topic;
using YourNotes.Communication.Responses.Topic;
using YourNotes.Domain.Entities;

namespace YourNotes.Domain.Interfaces.UseCases
{
    public interface IUpdateTopicUseCase
    {

        public Task<ResponseTopicJson> Execute(RequestUpdateTopicJson request);
        public Task<Topic> ValidateAndReturnTopic(RequestUpdateTopicJson request, User user);
    }
}
