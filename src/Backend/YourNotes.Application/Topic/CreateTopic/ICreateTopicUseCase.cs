using YourNotes.Communication.Requests.Topic;
using YourNotes.Communication.Responses.Topic;

namespace YourNotes.Application.Topic.CreateTopic
{
    public interface ICreateTopicUseCase
    {

        public Task<ResponseCreateTopicJson> Execute(RequestTopicJson request);

        public Task Validate(RequestTopicJson request, Guid userId);
    }
}
