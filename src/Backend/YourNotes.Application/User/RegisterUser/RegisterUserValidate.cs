using FluentValidation;
using YourNotes.Application.Services.ValidateAttribute;
using YourNotes.Communication.Requests.User;
using YourNotes.Exception;

namespace YourNotes.Application.User.RegisterUser
{
    public class RegisterUserValidate : AbstractValidator<RequestRegisterUser>
    {
        public RegisterUserValidate()
        {
            RuleFor(user => user.UserName)
                .NotEmpty()
                .NotNull()
                .WithMessage(YourNotesExceptionResource.USERNAME_INVALID);


            RuleFor(user => user.FirstName)
                .NotEmpty()
                .NotNull()
                .WithMessage(YourNotesExceptionResource.INVALID_FIRSTNAME);

            RuleFor(user => user.LastName)
               .NotEmpty()
               .NotNull()
               .WithMessage(YourNotesExceptionResource.INVALID_LASTNAME);

            RuleFor(user => user.Email)
                .SetValidator(new EmailValidator<RequestRegisterUser>());

            RuleFor(user => user.Password)
              .SetValidator(new PasswordValidator<RequestRegisterUser>());
        }
    }
}
