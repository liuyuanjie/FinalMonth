using System.Threading.Tasks;

namespace FinalMonth.gRPC.ClientApi.Services
{
    public interface IGrpcService
    {
        Task<HelloReply> GetSayHelloGrpcAsync(HelloRequest request);
    }
}