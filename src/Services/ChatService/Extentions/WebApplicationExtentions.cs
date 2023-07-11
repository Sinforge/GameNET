using ChatService.Data;
using FluentMigrator.Runner;

namespace ChatService.Extentions
{
    public static class WebApplicationExtentions
    {
        public static void CreateMigrations(this WebApplication app, string dbName)
        {
            using (var scope = app.Services.CreateScope())
            {
                var dbService = scope.ServiceProvider.GetRequiredService<DatabaseManager>();
                var migrationService = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

                try
                {
                    dbService.CreateDatabase(dbName);
                    migrationService.ListMigrations();
                    migrationService.MigrateUp();
                }
                catch
                {
                    //log errors or ...
                    throw;
                }
            }

        }
    }
}
