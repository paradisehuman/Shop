using System.Text.Json.Serialization;
using Shop.Domain.Events;
using Shop.Domain.Events.Product;
using Shop.Domain.ValueObjects;

namespace Shop.Domain.Entities;

public class Product
{
    public Guid Id { get; private set; }
    public string Picture { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public Price Price { get; private set; }
    public int StockQuantity { get; private set; }
    
    private Product() { }

    public Product(string picture, string title, string description, Price price, int initialStock)
    {
        Picture = picture;
        Title = title;
        Description = description;
        Price = price;
        StockQuantity = initialStock;

        AddDomainEvent(new ProductCreatedEvent(this));
    }
    
    [JsonConstructor]
    public Product(Guid id, string picture, string title, string description, Price price, int stockQuantity)
    {
        Id = id;
        Picture = picture;
        Title = title;
        Description = description;
        Price = price;
        StockQuantity = stockQuantity;
    }
    public void IncreaseStock(int quantity)
    {
        if (quantity <= 0)
        {
            throw new ArgumentException("Quantity must be greater than zero.");
        }
        StockQuantity += quantity;

        AddDomainEvent(new ProductStockUpdatedEvent(this, quantity));
    }

    public void ReduceStock(int quantity)
    {
        if (quantity <= 0)
        {
            throw new ArgumentException("Quantity must be greater than zero.");
        }
        if (StockQuantity < quantity)
        {
            throw new InvalidOperationException("Insufficient stock.");
        }
        StockQuantity -= quantity;

        AddDomainEvent(new ProductStockUpdatedEvent(this, -quantity));
    }
    
    public bool IsInStock(int quantity)
    {
        return StockQuantity >= quantity;
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



