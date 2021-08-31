using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace CapApi.MiscConsumer.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PublishController : Controller
    {
        [HttpGet]
        [Route("check")]
        [CapSubscribe("xxx.services.show.time")]
        public void CheckReceivedMessage(DateTime? datetime)
        {
            using (var connection = new SqlConnection("Server=.;Database=FinalMonth;Trusted_Connection=True;"))
            {
            }

            Console.WriteLine(datetime);
        }
    }
}
