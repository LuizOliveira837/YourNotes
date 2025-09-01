using System.Net;
using YourNotes.Communication.Requests.Topic;
using YourNotes.Communication.Responses.Topic;
using YourNotes.Domain.Interfaces.Repositories;
using YourNotes.Domain.Interfaces.Repositories.Topic;
using YourNotes.Domain.Interfaces.Services;
using YourNotes.Domain.Interfaces.UseCases;
using YourNotes.Exception;
using YourNotes.Exception.Exceptions;

namespace YourNotes.Application.Topic.UpdateTopic
{
    public class UpdateTopicUseCase : IUpdateTopicUseCase
    {
        private readonly ILoggedUser _loggedUser;
        private readonly ITopicWriteOnlyRepository _writeRepository;
        private readonly ITopicReadOnlyRepository _readRepository;
        private readonly IUnitOfWork _uof;

        public UpdateTopicUseCase(ITopicWriteOnlyRepository writeRepository, ITopicReadOnlyRepository readRepository, ILoggedUser loggedUser, IUnitOfWork uof = null)
        {
            _writeRepository = writeRepository;
            _readRepository = readRepository;
            _loggedUser = loggedUser;
            _uof = uof;
        }
        public async Task<ResponseTopicJson> Execute(RequestUpdateTopicJson request)
        {
            var user = await _loggedUser.User();

            var topic = await ValidateAndReturnTopic(request, user);

            if (topic.Title == request.NewTitle) return new ResponseTopicJson(topic.Id, topic.Title);

            topic.Title = request.NewTitle;

            await _uof.Commit();

            return new ResponseTopicJson(topic.Id, topic.Title);
        }

        public async Task<YourNotes.Domain.Entities.Topic> ValidateAndReturnTopic(RequestUpdateTopicJson request, YourNotes.Domain.Entities.User user)
        {
            var validator = new UpdateTopicValidator();

            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                var error = result
                    .Errors
                    .Select(e => e.ErrorMessage)
                    .First();

                throw new OnValidationException(error);
            }


            var topic = await _readRepository.GetTopicByIdAndUserId(request.Id, user.Id)?? throw new OnValidationException(YourNotesExceptionResource.TOPIC_NOT_FOUND, HttpStatusCode.NotFound);
 

            return topic!;
        }
    }
}
