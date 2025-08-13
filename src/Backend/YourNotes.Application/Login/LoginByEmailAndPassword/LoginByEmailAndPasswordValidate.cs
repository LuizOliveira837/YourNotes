using FluentValidation;
using YourNotes.Communication.Requests.Login;
using YourNotes.Application.Services.ValidateAttribute;

namespace YourNotes.Application.Login.LoginByEmailAndPassword
{
    public class LoginByEmailAndPasswordValidate : AbstractValidator<RequestLoginByEmailAndPassword>
    {
        public LoginByEmailAndPasswordValidate()
        {
            RuleFor(l => l.Email)
                .SetValidator(new EmailValidator<RequestLoginByEmailAndPassword>());

            When(
                x => new EmailValidator<RequestLoginByEmailAndPassword>()
                    .IsValid(new ValidationContext<RequestLoginByEmailAndPassword>(x), x.Email),
                () =>
                {
                    RuleFor(l => l.Password)
                        .SetValidator(new PasswordValidator<RequestLoginByEmailAndPassword>());
                }
            );
        }
    }
}
