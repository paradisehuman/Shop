namespace Shop.Domain.Events.Product;

public class ProductStockUpdatedEvent(Entities.Product product, int quantityChange) : DomainEvent
{
    public Entities.Product Product { get; } = product;
    public int QuantityChange { get; } = quantityChange;
}
