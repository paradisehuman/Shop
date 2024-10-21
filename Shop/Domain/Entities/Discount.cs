using Shop.Domain.ValueObjects;

namespace Shop.Domain.Entities;

public class Discount
{
    public Guid Id { get; private set; }
    public Price Value { get; private set; }
    public string Title { get; private set; }

    private Discount() { } 

    public Discount(Price value, string title)
    {
        Value = value;
        Title = title;
    }

    public decimal CalculateDiscount(decimal total)
    {
        throw new NotImplementedException();
    }
}

