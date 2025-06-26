namespace Netby.Todo.Application
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Netby.Todo.Application.Services.Tasks;

    public static class ApplicationModule
    {

        public static IServiceCollection AddApplicationModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITaskService, TaskService>();
            return services;
        }
    }
}
