using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Netby.Todo.Site.API.Services.API;

namespace Netby.Todo.Site.API
{
    public static class SiteApiModule
    {
        public static IServiceCollection AddSiteApiModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<TaskApiService>();
            return services;
        }
    }
}
