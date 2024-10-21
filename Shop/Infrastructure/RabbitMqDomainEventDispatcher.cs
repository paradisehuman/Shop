using MassTransit;
using Shop.Domain.Events;
using Shop.Domain.Events.Basket;
using Shop.Domain.Events.Customer;
using Shop.Domain.Events.Product;
using Shop.Infrastructure.Contracts;

namespace Shop.Infrastructure;

public class RabbitMqDomainEventDispatcher(IPublishEndpoint publishEndpoint) : IDomainEventDispatcher
{
    public async Task Dispatch(DomainEvent domainEvent)
    {
        //todo: here we should use strategy patter
        switch (domainEvent)
        {
            case CustomerCreatedEvent customerCreatedEvent:
                await publishEndpoint.Publish(customerCreatedEvent);
                break;
            
            case ProductCreatedEvent productCreatedEvent:
                await publishEndpoint.Publish(productCreatedEvent);
                break;
            
            case ProductStockUpdatedEvent productStockUpdatedEvent:
                await publishEndpoint.Publish(productStockUpdatedEvent);
                break;
            
            case BasketCreatedEvent basketCreatedEvent:
                await publishEndpoint.Publish(basketCreatedEvent);
                break;
            
            case BasketItemAddedEvent basketItemAddedEvent:
                await publishEndpoint.Publish(basketItemAddedEvent);
                break;
            
            case BasketItemRemovedEvent basketItemRemovedEvent:
                await publishEndpoint.Publish(basketItemRemovedEvent);
                break;
            
            case DiscountAppliedEvent discountAppliedEvent:
                await publishEndpoint.Publish(discountAppliedEvent);
                break;

            default:
                throw new InvalidOperationException("Unknown domain event type.");
        }
    }
}