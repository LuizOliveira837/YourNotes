using FluentValidation;
using YourNotes.Communication.Requests.Article;
using YourNotes.Exception;

namespace YourNotes.Application.Article
{
    public class ValidateContent : AbstractValidator<RequestContentJson>
    {

        public ValidateContent()
        {
            RuleFor(c => c.Position)
                .GreaterThanOrEqualTo(1)
                .WithMessage(YourNotesExceptionResource.INVALID_CONTENT_POSITION);

            RuleFor(c => c.ArticleId)
                .NotEmpty()
                .WithMessage(YourNotesExceptionResource.INVALID_ARTICLE_ID);
        }
    }
}
