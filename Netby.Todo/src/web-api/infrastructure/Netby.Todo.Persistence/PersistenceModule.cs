namespace Netby.Todo.Persistence
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Netby.Todo.Application.Services.Repositories;
    using Netby.Todo.Persistence.Contexts;
    using Netby.Todo.Persistence.Repositories;

    public static class PersistenceModule
    {
        public static IServiceCollection AddPersistenceModule(this IServiceCollection services, IConfiguration configuration)
        {
            RegisterDbContexts(services, configuration);
            RegisterRepositories(services);

            return services;
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        private static void RegisterDbContexts(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<NetbyDbContext>(options =>
                            options.UseSqlServer(
                                configuration.GetConnectionString("Local"),
                                sqlOptions =>
                                    sqlOptions.MigrationsAssembly(typeof(NetbyDbContext).Assembly.FullName)
                            )
                        );
        }
    }
}
