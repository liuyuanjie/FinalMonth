using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using FinalMonth.Api.Notification;
using FinalMonth.Application.Command;
using FinalMonth.Application.Query;
using FinalMonth.Domain;
using MediatR;
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
