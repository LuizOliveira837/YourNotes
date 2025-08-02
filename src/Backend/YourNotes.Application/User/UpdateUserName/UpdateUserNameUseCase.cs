using YourNotes.Communication.Requests.User;
using YourNotes.Communication.Responses.User;
using YourNotes.Domain.Interfaces.Repositories;
using YourNotes.Domain.Interfaces.Services;
using YourNotes.Domain.Interfaces.UseCases;
using YourNotes.Exception;
using YourNotes.Exception.Exceptions;

namespace YourNotes.Application.User.UpdateUserName
{
    public class UpdateUserNameUseCase : IUpdateUserNameUseCase
    {
        private readonly IUnitOfWork _uof;
        private readonly ILoggedUser _loggedUser;

        public UpdateUserNameUseCase(IUnitOfWork uof, ILoggedUser loggedUser)
        {
            _uof = uof;
            _loggedUser = loggedUser;
        }
        public async Task<ResponseUpdateUserName> Execute(RequestUpdateUserName request)
        {
            //mapear

            var userLogged = await _loggedUser.User();

            var user = await _uof.Users.GetAsync(userLogged.Id);

            if (user!.UserName == request.UserName) return new ResponseUpdateUserName(user.UserName);

            //validar
            await Validate(request);

            user.UserName = request.UserName;

            //salvar
            await _uof
                .Commit();

            //retornar

            return new ResponseUpdateUserName(user.UserName);

        }

        public async Task Validate(RequestUpdateUserName request)
        {
            var validator = new UpdateUserNameValidate();


            var result = validator.Validate(request);

            if (result.IsValid is not true)
            {
                var error =
                    result
                    .Errors
                    .FirstOrDefault()!
                    .ErrorMessage;

                throw new OnValidationException(error);
            }

            var userNameAlreadyExists = await _uof
                 .Users
                 .UserNameExistsAsync(request.UserName);

            if (userNameAlreadyExists is true) throw new OnValidationException(YourNotesExceptionResource.USERNAME_ALREADY_EXISTS);


        }
    }
}
