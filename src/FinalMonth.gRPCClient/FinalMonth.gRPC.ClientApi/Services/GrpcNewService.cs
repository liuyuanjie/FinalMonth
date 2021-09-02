using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grpc.Net.ClientFactory;

namespace FinalMonth.gRPC.ClientApi.Services
{
    public class GrpcNewService : IGrpcNewService
    {
        private readonly Greeter.GreeterClient _client;

        public GrpcNewService(GrpcClientFactory grpcClientFactory)
        {
            _client = grpcClientFactory.CreateClient<Greeter.GreeterClient>("Greeter");
        }

        public async Task<HelloReply> GetSayHelloGrpcAsync(HelloRequest request)
        {
            var result = await _client.SayHelloAsync(request);

            return result;
        }
    }
}
