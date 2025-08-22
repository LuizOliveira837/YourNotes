using YourNotes.Communication.Requests.User;
using YourNotes.Communication.Responses.User;
using YourNotes.Domain.Interfaces.Repositories;
using YourNotes.Domain.Interfaces.Repositories.User;
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
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;

        public UpdateUserNameUseCase(IUnitOfWork uof, IUserReadOnlyRepository userReadOnlyRepository, IUserWriteOnlyRepository userWriteOnlyRepository, ILoggedUser loggedUser)
        {
            _uof = uof;
            _loggedUser = loggedUser;
            _userReadOnlyRepository = userReadOnlyRepository;
        }
        public async Task<ResponseUpdateUserName> Execute(RequestUpdateUserName request)
        {
            //mapear

            var user= await _loggedUser.User();

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

            var userNameAlreadyExists = await _userReadOnlyRepository
                 .UserNameExistsAsync(request.UserName);

            if (userNameAlreadyExists is true) throw new OnValidationException(YourNotesExceptionResource.USERNAME_ALREADY_EXISTS);


        }
    }
}
