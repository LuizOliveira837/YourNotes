using YourNotes.Communication.Responses.Topic;

namespace YourNotes.Domain.Interfaces.UseCases
{
    public interface IGetTopicUseCase
    {
        public Task<ResponseListTopicJson> Execute();
    }
}

