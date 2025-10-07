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
                .Length(4,30)
                .WithMessage(YourNotesExceptionResource.INVALID_ARTICLE_LENGTH);

            RuleFor(a => a.Description)
                .Length(1,100)
                .WithMessage(YourNotesExceptionResource.INVALID_DESCRIPTION_LENGTH);

        }
    }
}
