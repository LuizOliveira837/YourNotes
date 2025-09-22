using AutoMapper;
using YourNotes.Communication.Requests.Article;
using YourNotes.Communication.Requests.Topic;
using YourNotes.Communication.Requests.User;
using YourNotes.Communication.Responses.User;

namespace YourNotes.Application.Services.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            RequestToDomain();
            DomainToResponse();
        }

        public void RequestToDomain()
        {
            CreateMap<RequestRegisterUser, Domain.Entities.User>();
            CreateMap<RequestTopicJson, Domain.Entities.Topic>();
            CreateMap<RequestArticleJson, Domain.Entities.Article>();

        }

        public void DomainToResponse()
        {
            CreateMap<Domain.Entities.User, ResponseRegisterUser>();
            CreateMap<Domain.Entities.User, ResponseGetUser>();
        }
    }
}
