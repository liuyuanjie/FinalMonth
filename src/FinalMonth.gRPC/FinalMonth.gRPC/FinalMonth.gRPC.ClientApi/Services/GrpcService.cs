using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalMonth.gRPC.ClientApi.Services
{
    public class GrpcService : IGrpcService
    {
        private readonly Greeter.GreeterClient _client;

        public GrpcService(Greeter.GreeterClient client)
        {
            _client = client;
        }

        public async Task<HelloReply> GetSayHelloGrpcAsync(HelloRequest request)
        {
            var result = await _client.SayHelloAsync(request);

            return result;
        }
    }
}
