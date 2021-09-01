﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace CapApi.MiscConsumer.Api2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PublishController : Controller
    {
        [HttpGet]
        [Route("check")]
        [CapSubscribe("*.services.show.time")]
        public void CheckReceivedMessage1(DateTime? datetime)
        {
            using (var connection = new SqlConnection("Server=.;Database=FinalMonth;Trusted_Connection=True;"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT NEWID()";
                var guid = command.ExecuteScalar();
            }

            Console.WriteLine(datetime + "check1");
        }
    }
}