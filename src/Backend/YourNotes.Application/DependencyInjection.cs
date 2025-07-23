using Microsoft.Extensions.DependencyInjection;
using YourNotes.Application.Services;
using YourNotes.Application.User.RegisterUser;
using YourNotes.Domain.Interfaces.Repositories;
using YourNotes.Domain.Interfaces.UseCases;
using YourNotes.Persistence.Repositories;

namespace YourNotes.Application
{
    public static class DependencyInjection
    {
        public static void AddApplicationDependencyInjection(this IServiceCollection service)
        {


            AddRepositories(service);

            AddMapper(service);


        }


        public static void AddRepositories(this IServiceCollection service)
        {
            service
                .AddScoped<IRegisterUserUseCase, RegisterUserUseCase>()
                .AddScoped<IUserRespository, UserRepository>()
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<Domain.Interfaces.Repositories.IBaseRepository<YourNotes.Domain.Entities.User>, UserRepository>();
        }

        public static void AddMapper(this IServiceCollection service)
        {
            service
                .AddAutoMapper(config =>
                {
                    config.AddProfile(new MapperProfile());
                });
        }
    }
}
