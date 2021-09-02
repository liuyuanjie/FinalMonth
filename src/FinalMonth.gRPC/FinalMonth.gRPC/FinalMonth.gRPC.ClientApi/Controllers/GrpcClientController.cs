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
    public class GrpcClientController : Controller
    {
        private readonly IGrpcService _grpcService;

        public GrpcClientController(IGrpcService grpcService)
        {
            _grpcService = grpcService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetGrpc(string name)
        {
            var reply = await _grpcService.GetSayHelloGrpcAsync(new HelloRequest() {Name = name});

            return Ok(reply.Message);
        }
    }
}
