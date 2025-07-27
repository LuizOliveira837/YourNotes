using AutoMapper;
using YourNotes.Application.Services.Crypt;
using YourNotes.Communication.Requests.User;
using YourNotes.Communication.Responses;
using YourNotes.Communication.Responses.User;
using YourNotes.Domain.Interfaces.Repositories;
using YourNotes.Domain.Interfaces.UseCases;
using YourNotes.Exception;
using YourNotes.Exception.Exceptions;
using YourNotes.Persistence.Autentication.Tokens.Access.Generator;

namespace YourNotes.Application.User.RegisterUser
{
    public class RegisterUserUseCase : IRegisterUserUseCase
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;
        public readonly JwtTokenGenerator _tokenGenerator;
        public readonly PasswordEncrypter _passwordEncrypter;


        public RegisterUserUseCase(IMapper mapper, IUnitOfWork uof, PasswordEncrypter passwordEncrypter, JwtTokenGenerator tokenGenerator)
        {

            _mapper = mapper;
            _uof = uof;
            _passwordEncrypter = passwordEncrypter;
            _tokenGenerator = tokenGenerator;
        }
        public async Task<ResponseRegisterUser> Execute(RequestRegisterUser request)
        {
            //Validar Requisição
            await Validate(request);


            //CADASTRAR USUARIO

            var user = _mapper.Map<YourNotes.Domain.Entities.User>(request);

            user.Password = _passwordEncrypter.Encrypter(user.Password);
            var id = await _uof.Users.CreateAsync(user);


            await _uof.Commit();
            //RETORNAR USER

            var token = _tokenGenerator.GenerationToken(id);


            return new ResponseRegisterUser(id, new Token(token));
        }

        public async Task Validate(RequestRegisterUser request)
        {
            RegisterUserValidate validator = new();

            var result = validator.Validate(request);

            if (result.IsValid is false)
            {

                var error =
                    result
                    .Errors
                    .Select(x => x.ErrorMessage)
                    .First()
                    .ToString();


                throw new OnValidationException(error);
            }

            if (await _uof.Users.UserNameExistsAsync(request.UserName))
                throw new OnValidationException(YourNotesExceptionResource.USERNAME_ALREADY_EXISTS);


            if (await _uof.Users.EmailExistsAsync(request.Email))
                throw new OnValidationException(YourNotesExceptionResource.EMAIL_ALREADY_EXISTS);


        }
    }
}
