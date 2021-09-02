using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalMonth.gRPC.ClientApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinalMonth.gRPC.ClientApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GrpcNewClientController : Controller
    {
        private readonly IGrpcNewService _grpcNewService;

        public GrpcNewClientController(IGrpcNewService grpcNewService)
        {
            _grpcNewService = grpcNewService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetGrpc(string name)
        {
            var reply = await _grpcNewService.GetSayHelloGrpcAsync(new HelloRequest() {Name = name});

            return Ok(reply.Message);
        }
    }
}
