using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
