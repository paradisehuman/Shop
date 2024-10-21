using Shop.Domain.Entities;

namespace Shop.Domain.Events.Basket;

public class DiscountAppliedEvent : DomainEvent
{
    public Entities.Basket Basket { get; }
    public Discount Discount { get; }

    public DiscountAppliedEvent(Entities.Basket basket, Discount discount)
    {
        Basket = basket;
        Discount = discount;
    }
}