using Grpc.Net.Client;
using GrpcClient;
using System;
using System.Threading.Tasks;

namespace gRPC.Client
{
    /// <summary>
    /// Các kiểu dữ liệu trong file .proto 
    /// https://docs.microsoft.com/en-us/aspnet/core/grpc/protobuf?view=aspnetcore-3.1
    /// </summary>
    class Program
    {
        static async Task Main(string[] args)
        {
            // The port number(5001) must match the port of the gRPC server.
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Greeter.GreeterClient(channel);
            var reply = await client.SayHelloAsync(
                              new HelloRequest { Name = "GreeterClient" });
            Console.WriteLine("Greeting: " + reply.Message);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
