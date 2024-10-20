namespace Shop.Domain.ValueObjects;

public class Price
{
    public decimal Value { get; private set; }

    private Price() { }

    public Price(decimal value)
    {
        if (value < 0)
        {
            throw new ArgumentException("Price cannot be negative.");
        }
        Value = value;
    }
}

