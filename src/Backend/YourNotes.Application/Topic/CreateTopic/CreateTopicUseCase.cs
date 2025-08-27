using AutoMapper;
using YourNotes.Communication.Requests.Topic;
using YourNotes.Communication.Responses.Topic;
using YourNotes.Domain.Interfaces.Repositories;
using YourNotes.Domain.Interfaces.Repositories.Topic;
using YourNotes.Domain.Interfaces.Services;
using YourNotes.Domain.Interfaces.UseCases;
using YourNotes.Exception;
using YourNotes.Exception.Exceptions;

namespace YourNotes.Application.Topic.CreateTopic
{
    public class CreateTopicUseCase : ICreateTopicUseCase
    {
        private readonly IUnitOfWork _uof;
        private readonly ILoggedUser _loggedUser;
        private readonly ITopicReadOnlyRepository _readRepository;
        private readonly ITopicWriteOnlyRepository _writeRepository;
        private readonly IMapper _mapper;

        public CreateTopicUseCase(IUnitOfWork uof, ILoggedUser loggedUser, IMapper mapper, ITopicReadOnlyRepository readRepository, ITopicWriteOnlyRepository writeRepository)
        {
            _uof = uof;
            _loggedUser = loggedUser;
            _readRepository = readRepository;
            _writeRepository = writeRepository;
            _mapper = mapper;
        }
        public async Task<ResponseTopicJson> Execute(RequestTopicJson request)
        {
            var user = await _loggedUser.User();

            await Validate(request, user.Id);

            var topic = _mapper.Map<YourNotes.Domain.Entities.Topic>(request);

            topic.UserId = user.Id;

            var id = await _writeRepository.CreateAsync(topic);

            await _uof.Commit();

            return new ResponseTopicJson(id, topic.Title);
        }

        public async Task Validate(RequestTopicJson request, Guid userId)
        {
            ValidateTopic validate = new();

            var result = validate.Validate(request);

            if (!result.IsValid)
            {
                var errorMessage = result.Errors.First().ErrorMessage;
                throw new OnValidationException(errorMessage);
            }

            if (await _readRepository.TopicAlreadyExists(request.Title, userId))
            {
                throw new OnValidationException(YourNotesExceptionResource.TITLE_ALREADY_EXISTS);
            }
        }
    }
}
