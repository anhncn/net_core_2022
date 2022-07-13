using Grpc.Core;
using GrpcService;
using System.Threading.Tasks;

namespace NNanh.Zolo.Controllers.gRPC
{
    public class GreeterService : Greeter.GreeterBase
    {
        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }
    }
}
