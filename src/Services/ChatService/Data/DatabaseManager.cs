using Dapper;

namespace ChatService.Data
{

    public class DatabaseManager
    {
        private readonly ApplicationContext _context;
        public DatabaseManager(ApplicationContext context)
        {
            _context = context;
        }
        public void CreateDatabase(string dbName)
        {
            var query = "SELECT datname FROM pg_catalog.pg_database WHERE lower(datname) = lower(@name);";
            using (var connection = _context.CreateConnection())
            {
                var records = connection.Query(query, new {name = dbName});
                //create a new database if not exist
                if (!records.Any())
                    connection.Execute($"CREATE DATABASE {dbName}");
            }
        }
    }
}