using AutoMapper;
using YourNotes.Communication.Requests.User;
using YourNotes.Communication.Responses.User;

namespace YourNotes.Application.Services.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<RequestRegisterUser, Domain.Entities.User>();
            CreateMap<Domain.Entities.User, ResponseRegisterUser>();
        }
    }
}
