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
        private readonly IConfiguration _configuration;

        public NotificationMessageQuery(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<NotificationMessage>> GetAllAsync()
        {
            var sql = "SELECT * FROM [NotificationMessages]";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<NotificationMessage>(sql);
                return result.ToList();
            }
        }

        public async Task<NotificationMessage> GetByIdAsync(string id)
        {
            var sql = "SELECT * FROM [NotificationMessages] WHERE NotificationId = @Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<NotificationMessage>(sql, new { Id = id });
                return result;
            }
        }
    }
}
