using FluentValidation;
using YourNotes.Application.Services.ValidateAttribute;
using YourNotes.Communication.Requests.User;

namespace YourNotes.Application.User.ChangePassword
{
    public class ChangePasswordValidate : AbstractValidator<RequestChangePassword>
    {

        public ChangePasswordValidate()
        {
            RuleFor(u => u.NewPassword)
                .SetValidator(new PasswordValidator<RequestChangePassword>());
        }
    }
}
