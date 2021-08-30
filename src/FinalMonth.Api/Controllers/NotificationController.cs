using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalMonth.Api.SignalR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace FinalMonth.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationController
    {
        //private readonly Hub<IChatHub> _chatHub;

        //public NotificationController(Hub<IChatHub> chatHub)
        //{
        //    _chatHub = chatHub;
        //}

        //[HttpPost]
        //[Route("")]
        //public async Task Send(string user, [FromBody] string message)
        //{
        //    await _chatHub.(user, message);
        //}
        
    }
}
