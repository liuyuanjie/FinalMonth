using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using FinalMonth.Application.Repository;
using FinalMonth.Domain;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace FinalMonth.Infrastructure.Dapper
{
    public class NotificationMessageQuery : INotificationMessageQuery
    {
        private readonly IMSSqlConnection _msSqlConnection;
        private readonly IFinalMonthIDbContextProvider _dbContextProvider;
        private readonly string _connectionString;

        public NotificationMessageQuery(IConfiguration configuration, IMSSqlConnection msSqlConnection, IFinalMonthIDbContextProvider dbContextProvider)
        {
            _msSqlConnection = msSqlConnection;
            _dbContextProvider = dbContextProvider;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<NotificationMessage>> GetAllAsync()
        {
            var sql = "SELECT * FROM [NotificationMessages]";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<NotificationMessage>(sql);
                return result.ToList();
            }
        }

        public async Task<List<NotificationMessage>> GetTopTenAsync()
        {
            var sql = "SELECT top 10 * FROM [NotificationMessages]";
            var result = await _msSqlConnection.OpenConnectionAsync().QueryAsync<NotificationMessage>(sql);
            return result.ToList();
        }

        public async Task<List<NotificationMessage>> GetTopOneAsync()
        {
            var sql = "SELECT top 1 * FROM [NotificationMessages]";
            var result = await _dbContextProvider.DbConnection.QueryAsync<NotificationMessage>(sql);
            return result.ToList();
        }

        public async Task<NotificationMessage> GetByIdAsync(string id)
        {
            var sql = "SELECT * FROM [NotificationMessages] WHERE NotificationId = @Id";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<NotificationMessage>(sql, new { Id = id });
                return result;
            }
        }
    }
}
