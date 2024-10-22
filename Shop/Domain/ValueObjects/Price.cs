namespace Shop.Domain.ValueObjects;

public class Price
{
    public decimal Value { get; private set; }

    private Price() { }

    public Price(decimal value)
    {
        if (value < 0) throw new ArgumentException("Price cannot be negative.");
        Value = value;
    }

    public Price Multiply(decimal multiplier)
    {
        return new Price(Value * multiplier);
    }

    public Price Subtract(Price other)
    {
        return new Price(Value - other.Value);
    }

    public override string ToString() => Value.ToString("C");

    public Price Add(Price other)
    {
        return new Price(Value + other.Value);
    }
}

