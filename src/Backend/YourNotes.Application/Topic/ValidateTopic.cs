using FluentValidation;
using YourNotes.Communication.Requests.Topic;
using YourNotes.Exception;

namespace YourNotes.Application.Topic
{
    public class ValidateTopic : AbstractValidator<RequestTopicJson>
    {
        public ValidateTopic()
        {
            RuleFor(t => t.Title)
                .NotEmpty().WithMessage(YourNotesExceptionResource.INVALID_TITLE)
                .NotNull().WithMessage(YourNotesExceptionResource.INVALID_TITLE);

            RuleFor(t => t.Description)
                .NotEmpty().WithMessage(YourNotesExceptionResource.INVALID_TOPIC_DESCRIPTION);
        }
    }
}
