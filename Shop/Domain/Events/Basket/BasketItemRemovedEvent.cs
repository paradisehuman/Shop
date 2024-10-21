using Shop.Domain.Entities;

namespace Shop.Domain.Events.Basket;

public class BasketItemRemovedEvent : DomainEvent
{
    public Entities.Basket Basket { get; }
    public BasketItem Item { get; }

    public BasketItemRemovedEvent(Entities.Basket basket, BasketItem item)
    {
        Basket = basket;
        Item = item;
    }
}