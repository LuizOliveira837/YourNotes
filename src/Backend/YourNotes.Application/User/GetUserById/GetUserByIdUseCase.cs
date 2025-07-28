using AutoMapper;
using YourNotes.Communication.Responses;
using YourNotes.Domain.Interfaces.Repositories;
using YourNotes.Domain.Interfaces.UseCases;
using YourNotes.Exception;
using YourNotes.Exception.Exceptions;

namespace YourNotes.Application.User.GetUserById
{
    public class GetUserByIdUseCase : IGetUserByIdUseCase
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public GetUserByIdUseCase(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }
        public async Task<ResponseGetUser> Execute(Guid Id)
        {
            Validate(Id);

            var user = await _uof
                .Users
                .GetAsync(Id);

            var response = _mapper.Map<ResponseGetUser>(user);

            return response;

        }

        public void Validate(Guid Id)
        {

            if (Id.Equals(Guid.Empty)) throw new OnValidationException(YourNotesExceptionResource.USERNAME_INVALID);

        }
    }
}
