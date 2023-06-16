using AccountService.Data;
using AccountService.Services;
using FluentMigrator.Runner;
using Shared.Database;
using System.Reflection;

namespace Shared.Extentions
{
    public static class DatabaseServiceCollectionExtentions
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
