using Shop.Domain.Entities;

namespace Shop.Domain.Events.Basket;

public class DiscountAppliedEvent : DomainEvent
{
    public Entities.Basket Basket { get; }
    public Entities.Discount Discount { get; }

    public DiscountAppliedEvent(Entities.Basket basket, Entities.Discount discount)
    {
        Basket = basket;
        Discount = discount;
    }
}