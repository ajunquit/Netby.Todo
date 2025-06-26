using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Netby.Todo.Site.API.Services.API.Auth;
using Netby.Todo.Site.API.Services.API.Tasks;

namespace Netby.Todo.Site.API
{
    public static class SiteApiModule
    {
        public static IServiceCollection AddSiteApiModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<TaskApiService>();
            services.AddScoped<AuthApiService>();
            
            return services;
        }
    }
}
