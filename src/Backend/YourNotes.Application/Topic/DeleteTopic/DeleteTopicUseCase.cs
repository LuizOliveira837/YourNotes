using System.Net;
using YourNotes.Domain.Interfaces.Repositories;
using YourNotes.Domain.Interfaces.Repositories.Topic;
using YourNotes.Domain.Interfaces.Services;
using YourNotes.Domain.Interfaces.UseCases;
using YourNotes.Exception;
using YourNotes.Exception.Exceptions;

namespace YourNotes.Application.Topic.DeleteTopic
{
    public class DeleteTopicUseCase : IDeleteTopicUseCase
    {
        private readonly ILoggedUser _loggedUser;
        private readonly ITopicWriteOnlyRepository _writeRepository;
        private readonly IUnitOfWork _uof;

        public DeleteTopicUseCase(ILoggedUser loggedUser, ITopicWriteOnlyRepository writeRepository, IUnitOfWork uof)
        {
            _loggedUser = loggedUser;
            _writeRepository = writeRepository;
            _uof = uof;
        }
        public async Task Execute(Guid id)
        {
            var user = await _loggedUser.User();

            var topic = await _writeRepository.GetTopicByIdAndUserId(id, user.Id);

            if (topic is null)
            {
                throw new OnValidationException(YourNotesExceptionResource.TOPIC_NOT_EXISTS, HttpStatusCode.NotFound);
            }

            _writeRepository.Delete(topic);

            await _uof.Commit();
        }


    }
}
