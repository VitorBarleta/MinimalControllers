namespace TraditionalControllers.TestHarness.Models;

public class Customer
{
    public Guid Id { get; }
    public string Name { get; init; } = null!;

    public Customer()
    {
        Id = Guid.NewGuid();
    }
}
