using YourNotes.Application.Services.Crypt;
using YourNotes.Communication.Requests.Login;
using YourNotes.Communication.Responses;
using YourNotes.Communication.Responses.User;
using YourNotes.Domain.Interfaces.Repositories.User;
using YourNotes.Domain.Interfaces.UseCases;
using YourNotes.Exception;
using YourNotes.Exception.Exceptions;
using YourNotes.Persistence.Autentication.Tokens.Access.Generator;

namespace YourNotes.Application.Login.LoginByEmailAndPassword
{
    public class LoginByEmailAndPasswordUseCase : ILoginByEmailAndPasswordUseCase
    {
        private readonly PasswordEncrypter _encrypter;
        private readonly JwtTokenGenerator _tokenGenerator;
        private readonly IUserReadOnlyRepository _readOnlyRepository;

        public LoginByEmailAndPasswordUseCase(IUserReadOnlyRepository readOnlyRepository, PasswordEncrypter encrypter, JwtTokenGenerator tokenGenerator)
        {
            _encrypter = encrypter;
            _tokenGenerator = tokenGenerator;
            _readOnlyRepository = readOnlyRepository;
        }

        public async Task<ResponseRegisterUser> Execute(RequestLoginByEmailAndPassword request)
        {
            var user = await ValidateAndReturnUser(request);

            var token = _tokenGenerator.GenerationToken(user.Id);

            return new ResponseRegisterUser(user.Id, new Token(token));

        }
        public async Task<YourNotes.Domain.Entities.User> ValidateAndReturnUser(RequestLoginByEmailAndPassword request)
        {
            var validator = new LoginByEmailAndPasswordValidate();

            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                var error =
                            result
                            .Errors
                            .Select(e => e.ErrorMessage)
                            .FirstOrDefault();


                throw new OnValidationException(error ?? "");
            }

            var user = await _readOnlyRepository.UserExistsByEmailAndPassword(request.Email, _encrypter.Encrypter(request.Password));

            return user is null ? throw new OnAuthorizationException(YourNotesExceptionResource.USER_NOT_FOUND) : user;
        }
    }
}
