using AutoMapper;
using YourNotes.Communication.Responses.User;
using YourNotes.Domain.Interfaces.Services;
using YourNotes.Domain.Interfaces.UseCases;
using YourNotes.Exception;
using YourNotes.Exception.Exceptions;

namespace YourNotes.Application.User.GetUserById
{
    public class GetUserByIdUseCase : IGetUserByIdUseCase
    {
        private readonly ILoggedUser _loggedUser;
        private readonly IMapper _mapper;

        public GetUserByIdUseCase(ILoggedUser loggedUser, IMapper mapper)
        {
            _loggedUser = loggedUser;
            _mapper = mapper;
        }
        public async Task<ResponseGetUser> Execute()
        {
            var user = await _loggedUser.User();

            Validate(user.Id);

            var response = _mapper.Map<ResponseGetUser>(user);

            return response;

        }

        public void Validate(Guid Id)
        {

            if (Id.Equals(Guid.Empty)) throw new OnValidationException(YourNotesExceptionResource.USERNAME_INVALID);

        }
    }
}
