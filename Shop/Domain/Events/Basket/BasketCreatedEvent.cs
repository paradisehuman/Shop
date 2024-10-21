using System.Text.Json.Serialization;

namespace Shop.Domain.Events.Basket;

public class BasketCreatedEvent : DomainEvent
{
    public Guid BasketId { get; }

    public BasketCreatedEvent(Entities.Basket basket)
    {
        BasketId = basket.Id;
    }
    
    [JsonConstructor]
    public BasketCreatedEvent(Guid basketId)
    {
        BasketId = basketId;
    }
}