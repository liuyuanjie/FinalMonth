using System.Threading.Tasks;

namespace FinalMonth.gRPC.ClientApi.Services
{
    public interface IGrpcNewService
    {
        Task<HelloReply> GetSayHelloGrpcAsync(HelloRequest request);
    }
}