using Shop.Domain.Entities;

namespace Shop.Domain.Events.Basket;

public class BasketItemAddedEvent : DomainEvent
{
    public Entities.Basket Basket { get; }
    public BasketItem Item { get; }

    public BasketItemAddedEvent(Entities.Basket basket, BasketItem item)
    {
        Basket = basket;
        Item = item;
    }
}