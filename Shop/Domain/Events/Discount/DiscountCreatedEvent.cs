namespace Shop.Domain.Events.Discount;

public class DiscountCreatedEvent : DomainEvent
{
    public Entities.Discount Discount { get; }

    public DiscountCreatedEvent(Entities.Discount discount)
    {
        Discount = discount;
    }
}