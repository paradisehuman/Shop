using Shop.Domain.Events;
using Shop.Domain.Events.Discount;
using Shop.Domain.ValueObjects;

namespace Shop.Domain.Entities;

public class Discount
{
    public Guid Id { get; private set; }
    public decimal Value { get; private set; }
    public string Title { get; private set; }
    public Guid CustomerId { get; private set; }
    public Customer Customer { get; private set; }

    private Discount() { } 

    public Discount(decimal value, string title, Guid customerId)
    {
        Value = value;
        Title = title;
        CustomerId = customerId;
        
        AddDomainEvent(new DiscountCreatedEvent(this));
    }

    private readonly List<DomainEvent> _domainEvents = [];
    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    protected void AddDomainEvent(DomainEvent eventItem)
    {
        _domainEvents.Add(eventItem);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}

