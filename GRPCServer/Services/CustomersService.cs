using Grpc.Core;
using GRPCServer.Protos;
using System.ComponentModel.DataAnnotations;

namespace GRPCServer.Services
{
    public class CustomersService : Customer.CustomerBase
    {
        private readonly ILogger<CustomersService> _logger;
        public CustomersService(ILogger<CustomersService> logger)
        {
            _logger = logger;
        }

        public override Task<CustomerModel> GetCustomersInfo(CustomerLookupModel request, ServerCallContext context)
        {
            CustomerModel Output = new CustomerModel();
            
            if (request.UserId == 1)
            {
                Output.FirstName = "Jamie";
                Output.LastName = "Smith";
            }
            else if (request.UserId == 2)
            {
                Output.FirstName = "Jane";
                Output.LastName = "Doe";
            }
            else
            {
                Output.FirstName = "Greg";
                Output.LastName = "Thomas";
            }
            return Task.FromResult(Output);
        }

        public override async Task GetNewCustomers(newCustomerRequest request, IServerStreamWriter<CustomerModel> responseStream, ServerCallContext context)
        {
            List<CustomerModel> Customers = new List<CustomerModel>()
            {
                new CustomerModel
                {
                    FirstName = "Tim",
                    LastName = "Corey",
                    EmailAddress = "tim@iamtimcorey.com",
                    Age= 30,
                    IsAlive= true,
                },
                 new CustomerModel
                 {
                     FirstName = "iury",
                     LastName = "silva",
                     EmailAddress = "iury@iamtimcorey.com",
                     Age = 22,
                     IsAlive = false,
                 },
                 new CustomerModel
                 {
                     FirstName = "rafeu",
                     LastName = "lisboa",
                     EmailAddress = "rafeu@iamtimcorey.com",
                     Age = 22,
                     IsAlive = false,
                 },
            };

            foreach (var customer in Customers)
            {
                await responseStream.WriteAsync(customer);
            }
        }
    }
}
