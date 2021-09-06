using System.Data;
using System.Threading.Tasks;
using FinalMonth.Application.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace FinalMonth.Infrastructure.Dapper
{
    public class MSSqlConnection : IMSSqlConnection
    {
        private readonly IConfiguration _configuration;

        public MSSqlConnection(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection OpenConnection()
        {
            var sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            sqlConnection.Open();
            return sqlConnection;
        }
    }
}