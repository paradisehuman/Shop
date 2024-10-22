namespace Shop.Domain.Events.Basket;

public class BasketCompletedEvent : DomainEvent
{
    public Entities.Basket Basket { get; }

    public BasketCompletedEvent(Entities.Basket basket)
    {
        Basket = basket;
    }
}
