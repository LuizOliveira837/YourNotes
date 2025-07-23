using FluentValidation;
using YourNotes.Communication.Request;
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
               .NotEmpty()
               .NotNull()
               .Matches("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$")
               .WithMessage(YourNotesExceptionResource.INVALID_EMAIL);

            RuleFor(user => user.Password)
              .NotEmpty()
              .NotNull()
              .Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{10,}$")
              .WithMessage(YourNotesExceptionResource.INVALID_PASSWORD);
        }
    }
}
