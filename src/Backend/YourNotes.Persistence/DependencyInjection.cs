using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using YourNotes.Domain.Interfaces.Security;
using YourNotes.Domain.Interfaces.Services;
using YourNotes.Persistence.Autentication.Tokens.Access.Generator;
using YourNotes.Persistence.Autentication.Tokens.Access.Validator;
using YourNotes.Persistence.Data;
using YourNotes.Persistence.Data.Migrations.Tables;

namespace YourNotes.Persistence
{
    public static class DependencyInjection
    {
        public static void AddPersistenceDependencyInjection(this IServiceCollection service, IConfiguration configuration)
        {
            AddDbContext(service, configuration);
            AddJwtToken(service, configuration);
            AddMigrations(service, configuration);
        }

        private static void AddJwtToken(IServiceCollection service, IConfiguration configuration)
        {
            var signingKey = configuration.GetSection("Jwt:SigningKey").Value!.ToString();
            var expirationTimeInMinutes = int.Parse(configuration.GetSection("Jwt:ExpirationTimeInMinutes").Value!);

            service
                .AddScoped(opt => new JwtTokenGenerator(signingKey, expirationTimeInMinutes))
                .AddScoped<IJwtTokenValidator, JwtTokenValidator>(opt => new JwtTokenValidator(signingKey))
                .AddScoped<ILoggedUser, LoggedUser>();
        }

        private static void AddDbContext(IServiceCollection service, IConfiguration configuration)
        {
            service
                .AddDbContext<YourNotesDbContext>(opt =>
                {
                    opt.UseSqlServer(configuration.GetConnectionString("SqlServer"));
                });
        }

        private static void AddMigrations(IServiceCollection service, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SqlServer");
            service
                .AddFluentMigratorCore()
                .ConfigureRunner(opt =>
                {
                    opt.AddSqlServer2016();
                    opt.WithGlobalConnectionString(connectionString);
                    opt.ScanIn(Assembly.GetExecutingAssembly()).For.Migrations();
                });
        }
    }
}
