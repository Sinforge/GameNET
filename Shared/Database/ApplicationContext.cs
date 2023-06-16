using AccountService.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Data;

namespace Shared.Database
{
    public class ApplicationContext {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public ApplicationContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }
        public IDbConnection CreateConnection()
            => new NpgsqlConnection(_connectionString);


    }
}
