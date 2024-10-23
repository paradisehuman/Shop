using Shop.Domain.Events;
using Shop.Domain.Events.Product;
using Shop.Domain.Events.Stock;

namespace Shop.Domain.Entities;

public class Stock
{
    public int Quantity { get; private set; }
    
    private Stock()
    {
    }

    public Stock(int quantity)
    {
        Quantity = quantity;
    }

    public void IncreaseStock(int quantity)
    {
        if (quantity <= 0)
        {
            throw new ArgumentException("Quantity must be greater than zero.");
        }

        Quantity += quantity;

        AddDomainEvent(new ProductStockUpdatedEvent(quantity));
    }

    public void ReduceStock(int quantity)
    {
        if (quantity <= 0)
        {
            throw new ArgumentException("Quantity must be greater than zero.");
        }

        if (Quantity < quantity)
        {
            throw new InvalidOperationException("Insufficient stock.");
        }

        Quantity -= quantity;

        AddDomainEvent(new ProductStockUpdatedEvent(-quantity));
    }

    public bool IsInStock(int quantity)
    {
        return Quantity >= quantity;
    }
    
    private readonly List<DomainEvent> _domainEvents = [];
    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    
    private void AddDomainEvent(DomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}