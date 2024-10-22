using Shop.Domain.Enums;
using Shop.Domain.Events;
using Shop.Domain.Events.Discount;

namespace Shop.Domain.Entities;

public class Discount
{
    public Guid Id { get; private set; }
    public decimal Value { get; private set; }
    public string Title { get; private set; }
    public Guid CustomerId { get; private set; }
    public Customer Customer { get; private set; }
    public DiscountStatus Status { get; private set; }

    private Discount() { } 

    public Discount(decimal value, string title, Guid customerId)
    {
        Value = value;
        Title = title;
        CustomerId = customerId;
        Status = DiscountStatus.Active;
        
        AddDomainEvent(new DiscountCreatedEvent(this));
    }
    
    public void MarkAsUsed()
    {
        if (Status != DiscountStatus.Active)
        {
            throw new InvalidOperationException("Discount is not active and cannot be used.");
        }

        Status = DiscountStatus.Used;
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

