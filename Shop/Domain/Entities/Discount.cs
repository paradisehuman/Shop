using Shop.Domain.ValueObjects;

namespace Shop.Domain.Entities;

public class Discount
{
    public Guid Id { get; private set; }
    public Price Value { get; private set; }  // Price is a value object representing the discount value
    public string Title { get; private set; }

    private Discount() { }  // EF Core requires a parameterless constructor

    public Discount(Price value, string title)
    {
        Id = Guid.NewGuid();
        Value = value;
        Title = title;
    }
}

