using ChatService.Data;
using FluentMigrator.Runner;
using System.Reflection;

namespace ChatService.Extentions
{
    public static class ServiceCollectionExtentions
    {
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
