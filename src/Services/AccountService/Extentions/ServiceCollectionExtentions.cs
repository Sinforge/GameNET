using AccountService.Data;
using AccountService.Services;
using EventBus.Abstractions;
using EventBusRabbitMQ;
using FluentMigrator.Runner;
using RabbitMQ.Client;
using System.Reflection;

namespace AccountService.Extentions
{
    public static class ServiceCollectionExtentions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
        public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ApplicationContext>();
            services.AddSingleton<DatabaseManager>();
            services.AddLogging(c => c.AddFluentMigratorConsole())
                    .AddFluentMigratorCore()
                    .ConfigureRunner(c => c.AddPostgres()
                        .WithGlobalConnectionString(configuration.GetConnectionString("DefaultConnection"))
                        .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations());
        }
        
    }
}
