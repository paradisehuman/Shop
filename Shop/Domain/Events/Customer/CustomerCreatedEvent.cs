namespace Shop.Domain.Events.Customer;

public class CustomerCreatedEvent(Entities.Customer customer) : DomainEvent
{
    public Entities.Customer Customer { get; } = customer;
}