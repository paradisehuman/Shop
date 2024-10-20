using Shop.Domain.Events;

namespace Shop.Infrastructure.Contracts;

public interface IDomainEventDispatcher
{
    Task Dispatch(DomainEvent domainEvent);
}