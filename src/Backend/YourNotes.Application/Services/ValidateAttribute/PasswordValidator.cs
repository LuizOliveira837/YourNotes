using FluentValidation;
using FluentValidation.Validators;
using System.Text.RegularExpressions;
using YourNotes.Exception;

namespace YourNotes.Application.Services.ValidateAttribute
{
    public class PasswordValidator<T> : PropertyValidator<T, string>
    {

        public override string Name => "PasswordValidator";


        public override bool IsValid(ValidationContext<T> context, string value)
        {
            var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z0-9]).{6,}$");

            if (!regex.IsMatch(value))
            {

                context.MessageFormatter.AppendArgument("ErrorMessage", YourNotesExceptionResource.INVALID_PASSWORD);
                return false;
            }

            return true;
        }

        protected override string GetDefaultMessageTemplate(string errorCode) => "{ErrorMessage}";


    }

}
