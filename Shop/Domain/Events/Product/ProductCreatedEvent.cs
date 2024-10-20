namespace Shop.Domain.Events.Product;

public class ProductCreatedEvent(Entities.Product product) : DomainEvent
{
    public Entities.Product Product { get; } = product;
}
