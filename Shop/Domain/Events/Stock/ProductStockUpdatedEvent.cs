namespace Shop.Domain.Events.Stock;

public class ProductStockUpdatedEvent(int quantityChange) : DomainEvent
{
    public int QuantityChange { get; } = quantityChange;
}
