using AutoMapper;
using YourNotes.Communication.Request;
using YourNotes.Communication.Responses;

namespace YourNotes.Application.Services
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<RequestRegisterUser, YourNotes.Domain.Entities.User>();
            CreateMap<YourNotes.Domain.Entities.User, ResponseRegisterUser>();
        }
    }
}
