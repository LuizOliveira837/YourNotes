using AutoMapper;
using YourNotes.Communication.Responses.Topic;
using YourNotes.Domain.Interfaces.Repositories.Topic;
using YourNotes.Domain.Interfaces.Services;
using YourNotes.Domain.Interfaces.UseCases;

namespace YourNotes.Application.Topic.GetTopic
{
    public class GetTopicUseCase : IGetTopicUseCase
    {
        private readonly IMapper _mapper;
        private readonly ILoggedUser _loggedUser;
        private readonly ITopicReadOnlyRepository _readRepository;

        public GetTopicUseCase(IMapper mapper, ILoggedUser loggedUser, ITopicReadOnlyRepository readRepository)
        {
            _mapper = mapper;
            _loggedUser = loggedUser;
            _readRepository = readRepository;
        }
        public async Task<ResponseListTopicJson> Execute()
        {
            var user = await _loggedUser.User();

            var topics = await _readRepository.GetTopics(user);


            return new ResponseListTopicJson
            {
                Topics = topics.Select(t => new ResponseTopicJson(t.Id, t.Title, t.Description)).ToList()
            };

        }
    }
}
