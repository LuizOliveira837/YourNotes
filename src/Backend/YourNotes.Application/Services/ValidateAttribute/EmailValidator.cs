using FluentValidation;
using FluentValidation.Validators;
using System.Text.RegularExpressions;
using YourNotes.Exception;

namespace YourNotes.Application.Services.ValidateAttribute
{
    public class EmailValidator<T> : PropertyValidator<T, string>
    {
        public override string Name => "EmailValidator";


        public override bool IsValid(ValidationContext<T> context, string email)
        {
            var regex = new Regex("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$");

            if (!regex.IsMatch(email))
            {
                context.MessageFormatter.AppendArgument("ErrorMessage", YourNotesExceptionResource.INVALID_EMAIL);
                return false;
            }

            return true;
        }

        protected override string GetDefaultMessageTemplate(string errorCode) => "{ErrorMessage}";

    }
}
