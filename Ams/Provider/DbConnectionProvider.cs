using Ams.Provider.Interfaces;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace Ams.Provider
{
    public class DbConnectionProvider : IDbConnectionProvider
    {
        private string _connectionString;

        public DbConnectionProvider(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");

            //_connectionString = configuration.GetConnectionString("Default") ?? throw new Exception("Please initialise connection string");
        }

        public IDbConnection GetConnection() => new NpgsqlConnection(_connectionString);
    }
}
