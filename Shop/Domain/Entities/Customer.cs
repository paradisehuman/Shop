namespace Shop.Domain.Entities;

public class Customer
{
    public Guid Id { get; private set; }
    public string FirstName { get; private set; }

    private Customer() { }

    public Customer(string firstName)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
    }
}

