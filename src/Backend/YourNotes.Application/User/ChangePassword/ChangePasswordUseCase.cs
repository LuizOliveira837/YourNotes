using YourNotes.Application.Services.Crypt;
using YourNotes.Communication.Requests.User;
using YourNotes.Domain.Interfaces.Repositories;
using YourNotes.Domain.Interfaces.Services;
using YourNotes.Domain.Interfaces.UseCases;
using YourNotes.Exception;
using YourNotes.Exception.Exceptions;

namespace YourNotes.Application.User.ChangePassword
{
    public class ChangePasswordUseCase : IChangePasswordUseCase
    {
        private readonly IUnitOfWork _uof;
        private readonly ILoggedUser _loggedUser;
        private readonly PasswordEncrypter _passwordEncrypter;

        public ChangePasswordUseCase(IUnitOfWork uof, ILoggedUser loggedUser, PasswordEncrypter passwordEncrypter)
        {
            _uof = uof;
            _loggedUser = loggedUser;
            _passwordEncrypter = passwordEncrypter;
        }
        public async Task Execute(RequestChangePassword request)
        {
            var user = await _loggedUser.User();

            Validate(request, user.Password);

            user.Password = _passwordEncrypter.Encrypter(request.NewPassword);

            await _uof.Commit();
        }

        public void Validate(RequestChangePassword request, string currentPassword)
        {

            ChangePasswordValidate validator = new();

            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                var error = result
                    .Errors
                    .Select(u => u.ErrorMessage)
                    .First();

                throw new OnValidationException(error);

            }


            if (_passwordEncrypter.Encrypter(request.Password) != currentPassword)
            {
                throw new OnValidationException(YourNotesExceptionResource.INVALID_PASSWORD);
            }
        }
    }
}
