using YourNotes.Communication.Requests.User;
using YourNotes.Communication.Responses.User;
using YourNotes.Domain.Interfaces.Repositories;
using YourNotes.Domain.Interfaces.UseCases;
using YourNotes.Exception;
using YourNotes.Exception.Exceptions;

namespace YourNotes.Application.User.UpdateUserName
{
    public class UpdateUserNameUseCase : IUpdateUserNameUseCase
    {
        private readonly IUnitOfWork _uof;

        public UpdateUserNameUseCase(IUnitOfWork uof)
        {
            _uof = uof;
        }
        public async Task<ResponseUpdateUserName> Execute(Guid id, RequestUpdateUserName request)
        {

            //mapear

            var user = await _uof.Users.GetAsync(id);

            if (user is null)
            {
                throw new OnUpdateValidation(YourNotesExceptionResource.USER_NOT_FOUND);
            }

            if (user.UserName == request.UserName) return new ResponseUpdateUserName(user.UserName);
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
