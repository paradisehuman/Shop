using Shop.Domain.Contracts;
using Shop.Domain.Events;
using Shop.Domain.Events.Customer;

namespace Shop.Domain.Entities;

public class Customer : IAggregateRoot
{
    public Guid Id { get; private set; }
    public string FirstName { get; private set; }

    private Customer()
    {
    }

    public Customer(string firstName)
    {
        FirstName = firstName;

        AddDomainEvent(new CustomerCreatedEvent(this));
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