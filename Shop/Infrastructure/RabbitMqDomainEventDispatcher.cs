using MassTransit;
using Shop.Domain.Events;
using Shop.Domain.Events.Customer;
using Shop.Infrastructure.Contracts;

namespace Shop.Infrastructure;

public class RabbitMqDomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IPublishEndpoint _publishEndpoint;

    public RabbitMqDomainEventDispatcher(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task Dispatch(DomainEvent domainEvent)
    {
        switch (domainEvent)
        {
            case CustomerCreatedEvent customerCreatedEvent:
                await _publishEndpoint.Publish(customerCreatedEvent);
                break;

            default:
                throw new InvalidOperationException("Unknown domain event type.");
        }
    }
}