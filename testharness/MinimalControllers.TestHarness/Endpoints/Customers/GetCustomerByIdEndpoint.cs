using MinimalControllers.Attributes;
using MinimalControllers.TestHarness.Models;
using MinimalControllers.TestHarness.Services;

namespace MinimalControllers.TestHarness.Endpoints.Customers;

[MinimalController("customer", HttpMethods.Get)]
public class GetCustomerByIdEndpoint : IEndpoint<Customer>
{
    private readonly IConfiguration config;
    private readonly ISomeService someService;

    public GetCustomerByIdEndpoint(IConfiguration config, ISomeService someService)
    {
        this.config = config;
        this.someService = someService;
    }

    public Task<Customer> HandleAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(new Customer() { Name = "Robson" });
    }
}
