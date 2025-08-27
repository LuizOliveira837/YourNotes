using YourNotes.Communication.Requests.Topic;
using YourNotes.Communication.Responses.Topic;

namespace YourNotes.Domain.Interfaces.UseCases
{
    public interface ICreateTopicUseCase
    {

        public Task<ResponseTopicJson> Execute(RequestTopicJson request);

        public Task Validate(RequestTopicJson request, Guid userId);
    }
}
