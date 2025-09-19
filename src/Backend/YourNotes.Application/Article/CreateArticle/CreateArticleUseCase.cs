using YourNotes.Communication.Requests.Article;
using YourNotes.Domain.Interfaces.Repositories.Topic;
using YourNotes.Domain.Interfaces.Services;
using YourNotes.Domain.Interfaces.UseCases;
using YourNotes.Exception;
using YourNotes.Exception.Exceptions;

namespace YourNotes.Application.Article.CreateArticle
{
    public class CreateArticleUseCase : ICreateArticleUseCase
    {
        private readonly ILoggedUser _loggedUser;
        public readonly ITopicReadOnlyRepository _readTopicRepository;

        public CreateArticleUseCase(ILoggedUser loggedUser, ITopicReadOnlyRepository readTopicRepository)
        {
            _loggedUser = loggedUser;
            _readTopicRepository = readTopicRepository;
        }
        public async Task Execute(RequestArticleJson request)
        {
            var user = await _loggedUser.User();

            await Validate(request, user.Id);

        }

        public async Task Validate(RequestArticleJson request, Guid userId)
        {
            var validator = new ValidateArticle();

            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                var error = result
                    .Errors
                    .Select(x => x.ErrorMessage)
                    .FirstOrDefault();

                throw new OnValidationException(error!);
            }

            if (! await _readTopicRepository.TopicExists(request.TopicId, userId))
            {
                throw new OnValidationException(YourNotesExceptionResource.TOPIC_NOT_FOUND);
            }
        }
    }
}
