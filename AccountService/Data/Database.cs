using Dapper;

namespace AccountService.Data
{
    public class Database
    {
        private readonly ApplicationContext _context;
        public Database(ApplicationContext context)
        {
            _context = context;
        }
        public void CreateDatabase(string dbName)
        {
            var query = "SELECT datname FROM pg_catalog.pg_database WHERE lower(datname) = lower(@name);\r\n";
            var parameters = new DynamicParameters();
            parameters.Add("name", dbName);
            using (var connection = _context.CreateConnection())
            {
                var records = connection.Query(query, parameters);
                //create a new database if not exist
                if (!records.Any())
                    connection.Execute($"CREATE DATABASE {dbName}");
            }
        }
    }
}
