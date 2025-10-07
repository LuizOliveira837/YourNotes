using AutoMapper;
using System.Net;
using System.Text.Json;
using YourNotes.Communication.Requests.Article;
using YourNotes.Domain.Entities;
using YourNotes.Domain.Interfaces.Repositories;
using YourNotes.Domain.Interfaces.Repositories.Article;
using YourNotes.Domain.Interfaces.Services;
using YourNotes.Domain.Interfaces.UseCases;
using YourNotes.Exception;
using YourNotes.Exception.Exceptions;

namespace YourNotes.Application.Article.CreateContent
{
    public class CreateContentUseCase : ICreateContentUseCase
    {
        private readonly ILoggedUser _loggedUser;
        private readonly IArticleReadOnlyRepository _readArticleRepository;
        private readonly IArticleWriteOnlyRepository _writeArticleRepository;
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public CreateContentUseCase(ILoggedUser loggedUser, IArticleReadOnlyRepository readArticleRepository, IMapper mapper, IArticleWriteOnlyRepository writeArticleRepository, IUnitOfWork uof)
        {
            _loggedUser = loggedUser;
            _readArticleRepository = readArticleRepository;
            _mapper = mapper;
            _writeArticleRepository = writeArticleRepository;
            _uof = uof;
        }
        public async Task Execute(RequestContentJson request)
        {
            var user = await _loggedUser.User();

            await Validate(request, user.Id);

            var content = _mapper.Map<Content>(request);

            content.Markup = JsonSerializer.Serialize(content.Markup);

            await _writeArticleRepository.CreateArticleContentAsync(content);

            await _uof.Commit();
        }

        public async Task Validate(RequestContentJson request, Guid userId)
        {
            var validator = new ValidateContent();

            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                var error = result
                    .Errors
                    .Select(x => x.ErrorMessage)
                    .FirstOrDefault();

                throw new OnValidationException(error ?? "");
            }


            //Verificar se o Article Existe

            if (!await _readArticleRepository.ArticleExists(request.ArticleId, userId))
            {
                throw new OnValidationException(YourNotesExceptionResource.ARTICLE_NOT_EXISTS, HttpStatusCode.NotFound);
            }
        }
    }
}
