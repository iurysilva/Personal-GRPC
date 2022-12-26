using Grpc.Core;
using Grpc.Net.Client;
using GRPCServer;
using GRPCServer.Protos;

namespace GRPCClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //var input = new HelloRequest { Name = "Iury" };

            //var Channel = GrpcChannel.ForAddress("https://localhost:7249");
            //var Client = new Greeter.GreeterClient(Channel);

            //var Reply = await Client.SayHelloAsync(input);
            //Console.WriteLine(Reply.Message);
            //Console.ReadLine();

            var Channel = GrpcChannel.ForAddress("https://localhost:7249");
            var CustomerClient = new Customer.CustomerClient(Channel);

            var ClientRequested = new CustomerLookupModel { UserId = 2 };
            var Customer = await CustomerClient.GetCustomersInfoAsync(ClientRequested);
            Console.WriteLine($"{Customer.FirstName} {Customer.LastName}");

            Console.WriteLine();
            Console.WriteLine("New Customer List");
            Console.WriteLine();

            using (var call = CustomerClient.GetNewCustomers(new newCustomerRequest()))
            {
                while (await call.ResponseStream.MoveNext())
                {
                    var currentCustomer = call.ResponseStream.Current;
                    Console.WriteLine($"{currentCustomer.FirstName} {currentCustomer.LastName}: {currentCustomer.EmailAddress}");
                }
            }

                Console.ReadLine();
        }
    }
}