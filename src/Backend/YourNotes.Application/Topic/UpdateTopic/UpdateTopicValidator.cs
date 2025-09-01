using FluentValidation;
using YourNotes.Communication.Requests.Topic;
using YourNotes.Exception;

namespace YourNotes.Application.Topic.UpdateTopic
{
    public class UpdateTopicValidator : AbstractValidator<RequestUpdateTopicJson>
    {
        public UpdateTopicValidator()
        {
            RuleFor(t => t.NewTitle)
               .NotEmpty().WithMessage(YourNotesExceptionResource.INVALID_TITLE)
               .NotNull().WithMessage(YourNotesExceptionResource.INVALID_TITLE);

        }
    }
}
