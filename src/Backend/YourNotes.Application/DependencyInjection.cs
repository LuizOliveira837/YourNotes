using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YourNotes.Application.Article.CreateArticle;
using YourNotes.Application.Login.LoginByEmailAndPassword;
using YourNotes.Application.Services.Crypt;
using YourNotes.Application.Services.Mapper;
using YourNotes.Application.Topic.CreateTopic;
using YourNotes.Application.Topic.GetTopic;
using YourNotes.Application.Topic.UpdateTopic;
using YourNotes.Application.User.ChangePassword;
using YourNotes.Application.User.DeleteUser;
using YourNotes.Application.User.GetUserById;
using YourNotes.Application.User.RegisterUser;
using YourNotes.Application.User.UpdateUserName;
using YourNotes.Domain.Interfaces.UseCases;


namespace YourNotes.Application
{
    public static class DependencyInjection
    {
        public static void AddApplicationDependencyInjection(this IServiceCollection service, IConfiguration configuration)
        {
            AddMapper(service);

            AddServiceCrypt(service, configuration);

            AddUseCases(service);
        }

        private static void AddUseCases(IServiceCollection service)
        {
            service
              .AddScoped<IRegisterUserUseCase, RegisterUserUseCase>()
              .AddScoped<IUpdateUserNameUseCase, UpdateUserNameUseCase>()
              .AddScoped<IGetUserByIdUseCase, GetUserByIdUseCase>()
              .AddScoped<IDeleteUserUseCase, DeleteUserUseCase>()
              .AddScoped<ILoginByEmailAndPasswordUseCase, LoginByEmailAndPasswordUseCase>()
              .AddScoped<IChangePasswordUseCase, ChangePasswordUseCase>()
              .AddScoped<ICreateTopicUseCase, CreateTopicUseCase>()
              .AddScoped<IGetTopicUseCase, GetTopicUseCase>()
              .AddScoped<IUpdateTopicUseCase, UpdateTopicUseCase>()
              .AddScoped<ICreateArticleUseCase, CreateArticleUseCase>();



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
