using FluentValidation;
using YourNotes.Communication.Requests.User;
using YourNotes.Exception;

namespace YourNotes.Application.User.UpdateUserName
{
    public class UpdateUserNameValidate : AbstractValidator<RequestUpdateUserName>
    {
        public UpdateUserNameValidate()
        {
            RuleFor
                (u => u.UserName)
                .NotEmpty()
                .NotNull()
                .WithName(YourNotesExceptionResource.USERNAME_INVALID);
        }
    }
}
