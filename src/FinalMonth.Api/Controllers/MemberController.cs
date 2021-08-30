using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FinalMonth.Api.Command;
using FinalMonth.Api.Filters;
using FinalMonth.Api.Notification;
using FinalMonth.Api.Query;
using FinalMonth.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalMonth.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MemberController : Controller
    {
        private readonly IMediator _mediator;

        public MemberController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddMember(AddMemberCommand command)
        {
            command.UserId = User.FindFirstValue(ClaimTypes.Sid);
            var result = await _mediator.Send(command);
            return result ? Ok() : BadRequest();
        }

        [HttpGet]
        public async Task<IList<Member>> GetMembers(GetMemberQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpGet]
        [Route("send")]
        public async Task<IActionResult> SendMember()
        {
            await _mediator.Publish(new SendMemberNotification());

            return new OkResult();
        }
    }
}
