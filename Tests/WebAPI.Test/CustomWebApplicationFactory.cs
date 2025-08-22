using CommonTestUtilities.Builders;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using YourNotes.Persistence.Data;

namespace WebAPI.Test
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        private readonly YourNotes.Domain.Entities.User _user;
        private readonly YourNotes.Domain.Entities.Topic _topic;

        public CustomWebApplicationFactory()
        {
            _user = UserBuilder.Build();
            _topic = new YourNotes.Domain.Entities.Topic()
            {
                Title = "Test",
            };

        }

        public string Email
        {
            get => _user.Email;
        }

        public string UserName
        {
            get => _user.UserName;
        }

        public string FirstName
        {
            get => _user.FirstName;
        }

        public string LastName
        {
            get => _user.LastName;
        }
        public bool Active
        {
            get => _user.Active;
        }

        public string Password
        {
            get => _user.Password;
        }
        public Guid Id
        {
            get => _user.Id;
        }

        public string Title
        {
            get => _topic.Title;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder
              .UseEnvironment("Test")
              .ConfigureServices(services =>
              {
                  var descriptor = services.SingleOrDefault(x => x.ServiceType == typeof(DbContextOptions<YourNotesDbContext>));

                  if (descriptor is not null)
                  {
                      services.Remove(descriptor);
                  }


                  var provider =
                  services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

                  services.AddDbContext<YourNotesDbContext>(opt =>
                  {
                      opt.UseInMemoryDatabase("InMemoryDbForTesting");
                      opt.UseInternalServiceProvider(provider);
                  });

                  var scope = services.BuildServiceProvider().CreateScope();

                  var context = scope.ServiceProvider.GetService<YourNotesDbContext>();

                  if (context is not null)
                  {
                      CreateUserTest(context);

                      CreateTopicTest(context);
                  }



              });

        }

        private void CreateUserTest(YourNotesDbContext context)
        {

            context
               .Users
               .Add(new YourNotes.Domain.Entities.User()
               {
                   UserName = UserName,
                   FirstName = FirstName,
                   LastName = LastName,
                   Email = Email,
                   Password = PasswordEncrypterBuilder.Build().Encrypter(Password),
                   Id = Id,
               });


            context
                .SaveChanges();
        }

        private void CreateTopicTest(YourNotesDbContext context)
        {

            context
               .Topics
               .Add(new YourNotes.Domain.Entities.Topic()
               {
                   Title = _topic.Title,
                   Id = Id,
                   UserId = _user.Id,
               });

            context
              .SaveChanges();

        }
    }
}
