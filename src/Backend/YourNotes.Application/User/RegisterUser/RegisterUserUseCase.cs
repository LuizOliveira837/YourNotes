using AutoMapper;
using YourNotes.Communication.Request;
using YourNotes.Communication.Responses;
using YourNotes.Domain.Interfaces.Repositories;
using YourNotes.Domain.Interfaces.UseCases;
using YourNotes.Exception;
using YourNotes.Exception.Exceptions;

namespace YourNotes.Application.User.RegisterUser
{
    public class RegisterUserUseCase : IRegisterUserUseCase
    {
        private readonly IUserRespository _userRespository;
        private readonly IBaseRepository<YourNotes.Domain.Entities.User> _repository;
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;


        public RegisterUserUseCase(IUserRespository userRespository, IBaseRepository<YourNotes.Domain.Entities.User> repository, IMapper mapper, IUnitOfWork uof)
        {
            _userRespository = userRespository;
            _repository = repository;
            _mapper = mapper;
            _uof = uof;
        }
        public async Task<ResponseRegisterUser> Execute(RequestRegisterUser request)
        {
            //Validar Requisição
            await Validate(request);


            //CADASTRAR USUARIO

            var user = _mapper.Map<YourNotes.Domain.Entities.User>(request);
            var id = await _repository.CreateAsync(user);


            await _uof.Commit();
            //RETORNAR USER


            return new ResponseRegisterUser(id);
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

            if (await _userRespository.UserNameExistsAsync(request.UserName))
                throw new OnValidationException(YourNotesExceptionResource.USERNAME_ALREADY_EXISTS);



            if (await _userRespository.EmailExistsAsync(request.Email))
                throw new OnValidationException(YourNotesExceptionResource.EMAIL_ALREADY_EXISTS);



        }
    }
}
