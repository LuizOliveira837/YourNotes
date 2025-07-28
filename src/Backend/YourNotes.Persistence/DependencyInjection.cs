using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YourNotes.Domain.Security;
using YourNotes.Persistence.Autentication.Tokens.Access.Generator;
using YourNotes.Persistence.Autentication.Tokens.Access.Validator;
using YourNotes.Persistence.Data;

namespace YourNotes.Persistence
{
    public static class DependencyInjection
    {
        public static void AddPersistenceDependencyInjection(this IServiceCollection service, IConfiguration configuration)
        {
            AddDbContext(service, configuration);
            AddJwtToken(service, configuration);
        }

        private static void AddJwtToken(IServiceCollection service, IConfiguration configuration)
        {
            var signingKey = configuration.GetSection("Jwt:SigningKey").Value!.ToString();
            var expirationTimeInMinutes = int.Parse(configuration.GetSection("Jwt:ExpirationTimeInMinutes").Value!);
            service
                .AddScoped(opt=> new JwtTokenGenerator(signingKey, expirationTimeInMinutes))


              .AddScoped<IJwtTokenValidator, JwtTokenValidator>(opt=> new JwtTokenValidator(signingKey));

        }

        private static void AddDbContext(IServiceCollection service, IConfiguration configuration)
        {
            service
                .AddDbContext<YourNotesDbContext>(opt =>
                {
                    opt.UseSqlServer(configuration.GetSection("ConnectionString:SqlServer").Value);
                });
        }
    }
}
