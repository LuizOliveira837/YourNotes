using FluentValidation;
using YourNotes.Communication.Requests.Login;
using YourNotes.Application.Services.ValidateAttribute;

namespace YourNotes.Application.Login.LoginByEmailAndPassword
{
    public class LoginByEmailAndPasswordValidate : AbstractValidator<RequestLoginByEmailAndPassword>
    {
        public LoginByEmailAndPasswordValidate()
        {
            RuleFor(l => l.Password)
                .SetValidator(new PasswordValidator<RequestLoginByEmailAndPassword>());

            RuleFor(l => l.Email)
                .SetValidator(new EmailValidator<RequestLoginByEmailAndPassword>());
        }
    }
}
