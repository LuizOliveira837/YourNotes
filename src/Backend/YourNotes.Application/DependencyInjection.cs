using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YourNotes.Application.Services.Crypt;
using YourNotes.Application.Services.Mapper;
using YourNotes.Application.User.GetUserById;
using YourNotes.Application.User.RegisterUser;
using YourNotes.Application.User.UpdateUserName;
using YourNotes.Domain.Interfaces.Repositories;
using YourNotes.Domain.Interfaces.UseCases;
using YourNotes.Persistence.Repositories;

namespace YourNotes.Application
{
    public static class DependencyInjection
    {
        public static void AddApplicationDependencyInjection(this IServiceCollection service, IConfiguration configuration)
        {

            AddRepositories(service);

            AddMapper(service);

            AddServiceCrypt(service, configuration);

            AddUseCases(service);

        }

        private static void AddUseCases(IServiceCollection service)
        {
            service
              .AddScoped<IRegisterUserUseCase, RegisterUserUseCase>()
              .AddScoped<IUpdateUserNameUseCase, UpdateUserNameUseCase>()
              .AddScoped<IGetUserByIdUseCase, GetUserByIdUseCase>();


        }

        public static void AddRepositories(this IServiceCollection service)
        {
            service
                .AddScoped<IBaseRepository<YourNotes.Domain.Entities.User>, UserRepository>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IUnitOfWork, UnitOfWork>();

        }

        public static void AddMapper(this IServiceCollection service)
        {
            service
                .AddAutoMapper(config =>
                {
                    config.AddProfile(new MapperProfile());
                });
        }

        public static void AddServiceCrypt(this IServiceCollection service, IConfiguration configuration)
        {
            var secretKey = configuration.GetSection("Crypt:SecretKey").Value;

            service
                .AddScoped<PasswordEncrypter>(opt => new PasswordEncrypter(secretKey!));
        }
    }
}
