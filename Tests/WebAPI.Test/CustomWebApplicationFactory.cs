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
        public CustomWebApplicationFactory()
        {
            _user = UserBuilder.Build();
        }

        public string Email
        {
            get => _user.Email;
        }

        public string UserName
        {
            get => _user.UserName;
        }

        public Guid Id
        {
            get => _user.Id;
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
                  }



              });

        }

        private void CreateUserTest(YourNotesDbContext context)
        {
            context
               .Users
               .Add(_user);

            context
                .SaveChanges();
        }
    }
}
