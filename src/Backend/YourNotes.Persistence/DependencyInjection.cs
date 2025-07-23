using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YourNotes.Persistence.Data;

namespace YourNotes.Persistence
{
    public static class DependencyInjection
    {
        public static void AddPersistenceDependencyInjection(this IServiceCollection service, IConfiguration configuration)
        {
            AddDbContext(service, configuration);
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
