using AutoMapper;
using YourNotes.Communication.Requests.Article;
using YourNotes.Domain.Interfaces.Repositories;
using YourNotes.Domain.Interfaces.Repositories.Article;
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
        private readonly IMapper _mapper;
        public  readonly ITopicReadOnlyRepository _readTopicRepository;
        public  readonly IArticleWriteOnlyRepository _writeArticleRepository;
        public  readonly IUnitOfWork _uof;

        public CreateArticleUseCase(ILoggedUser loggedUser, ITopicReadOnlyRepository readTopicRepository, IMapper mapper, IUnitOfWork uof, IArticleWriteOnlyRepository writeArticleRepository)
        {
            _loggedUser = loggedUser;
            _readTopicRepository = readTopicRepository;
            _mapper = mapper;
            _uof = uof;
            _writeArticleRepository = writeArticleRepository;
        }
        public async Task Execute(RequestArticleJson request)
        {
            var user = await _loggedUser.User();

            await Validate(request, user.Id);

            var article = _mapper.Map<YourNotes.Domain.Entities.Article>(request);

            await _writeArticleRepository.CreateAsync(article);

            await _uof.Commit();

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

            if (!await _readTopicRepository.TopicExists(request.TopicId, userId))
            {
                throw new OnValidationException(YourNotesExceptionResource.TOPIC_NOT_FOUND);
            }
        }
    }
}
