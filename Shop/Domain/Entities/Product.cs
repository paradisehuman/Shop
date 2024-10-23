using System.Text.Json.Serialization;
using Shop.Domain.Contracts;
using Shop.Domain.Events;
using Shop.Domain.Events.Product;
using Shop.Domain.ValueObjects;

namespace Shop.Domain.Entities;

public class Product : IAggregateRoot
{
    public Guid Id { get; private set; }
    public string Picture { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public Price Price { get; private set; }
    public Stock Stock { get; private set; }

    private Product()
    {
    }

    public Product(string picture, string title, string description, Price price, Stock initialStock)
    {
        Picture = picture;
        Title = title;
        Description = description;
        Price = price;
        Stock = initialStock;

        AddDomainEvent(new ProductCreatedEvent(this));
    }

    [JsonConstructor]
    public Product(Guid id, string picture, string title, string description, Price price, Stock stock)
    {
        Id = id;
        Picture = picture;
        Title = title;
        Description = description;
        Price = price;
        Stock = stock;
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