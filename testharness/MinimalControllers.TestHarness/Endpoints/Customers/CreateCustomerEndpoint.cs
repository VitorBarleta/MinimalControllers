using MinimalControllers.Attributes;
using MinimalControllers.TestHarness.Endpoints.Customers.Requests;
using MinimalControllers.TestHarness.Models;

namespace MinimalControllers.TestHarness.Endpoints.Customers;

[MinimalController("customer", HttpMethods.Post)]
public class CreateCustomerEndpoint : IEndpoint<CreateCustomerRequest, Customer>
{
    public Task<Customer> HandleAsync(CreateCustomerRequest createCustomerRequest, CancellationToken cancellationToken)
    {
        var newCustomer = new Customer { Name = createCustomerRequest.Name };

        return ValueTask.FromResult(newCustomer).AsTask();
    }
}
