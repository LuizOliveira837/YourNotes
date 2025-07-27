using AutoMapper;
using YourNotes.Application.Services.Mapper;

namespace CommonTestUtilities.Builders
{
    public static class MapperBuilder
    {

        public static IMapper Builder()
        {

            var mapper = new MapperConfiguration(opt =>
            {
                opt.AddProfile<MapperProfile>();
            }).CreateMapper();


            return mapper;
        }
    }
}
