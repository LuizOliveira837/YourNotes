using FluentValidation;
using YourNotes.Communication.Requests.Article;
using YourNotes.Exception;

namespace YourNotes.Application.Article
{
    public class ValidateArticle : AbstractValidator<RequestArticleJson>
    {

        public ValidateArticle()
        {
            RuleFor(a => a.Title)
                .NotEmpty()
                .WithMessage(YourNotesExceptionResource.EMPTY_ARTICLE_TITLE)
                .MaximumLength(100)
                .MinimumLength(10)
                .WithMessage(YourNotesExceptionResource.INVALID_ARTICLE_LENGTH);

            RuleFor(a => a.Description)
                .NotEmpty()
                .WithMessage(YourNotesExceptionResource.EMPTY_DESCRIPTION)
                .MaximumLength(100)
                .MinimumLength(10)
                .WithMessage(YourNotesExceptionResource.INVALID_DESCRIPTION_LENGTH);

        }
    }
}
