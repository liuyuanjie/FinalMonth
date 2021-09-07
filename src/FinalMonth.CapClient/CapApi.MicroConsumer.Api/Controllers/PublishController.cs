using System;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SqlStreamStore.Logging;

namespace CapApi.MicroConsumer.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PublishController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILoggerProvider _loggerProvider;
        private readonly ILogger<PublishController> _logger;

        public PublishController(IConfiguration configuration,ILoggerFactory loggerFactory, ILoggerProvider loggerProvider, ILogger<PublishController> logger)
        {
            _configuration = configuration;
            _loggerFactory = loggerFactory;
            _loggerProvider = loggerProvider;
            _logger = logger;
        }

        [HttpGet]
        [Route("check")]
        [CapSubscribe("xxx.services.show.time",Group = "group")]
        public void CheckReceivedMessage(string content)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnectionString")))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT NEWID()";
                var guid = command.ExecuteScalar();
            }

            _loggerProvider.CreateLogger("PublishController").LogInformation("provider:{content} from {where}",content, "group");
            _logger.LogInformation("logger: {content} from {where}",content, "group");
            _loggerFactory.CreateLogger<PublishController>().LogInformation("factory:{content} from {where}",content, "group");
        }

        [HttpGet]
        [Route("check1")]
        [CapSubscribe("xxx.services.show.time", Group = "group1")]
        public void CheckReceivedMessage1(string content)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnectionString")))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT NEWID()";
                var guid = command.ExecuteScalar();
            }

            _loggerProvider.CreateLogger("ConsoleLoggerProvider").LogInformation("{content} from {where}", content, "group1");

        }
    }
}
