using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Netby.Todo.Persistence.Contexts;

namespace Netby.Todo.Persistence
{
    public static class PersistenceModule
    {
        public static IServiceCollection AddPersistenceModule(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddDbContext<NetbyDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("Local"))
                );
            return services;
        }
    }
}
