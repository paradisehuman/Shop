namespace Shop.Domain.Events.Customer;

public class CustomerCreatedEvent : DomainEvent
{
    public Entities.Customer Customer { get; }

    public CustomerCreatedEvent(Entities.Customer customer)
    {
        Customer = customer;
    }
}