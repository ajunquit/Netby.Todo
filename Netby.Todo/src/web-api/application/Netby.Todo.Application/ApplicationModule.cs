namespace Netby.Todo.Application
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class ApplicationModule
    {

        public static IServiceCollection AddApplicationModule(this IServiceCollection services, IConfiguration configuration)
        {
            
            return services;
        }
    }
}
