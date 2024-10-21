using MassTransit;

namespace Shop.Domain.Events.Basket;

public class BasketCreatedEventConsumer : IConsumer<BasketCreatedEvent>
{
    public async Task Consume(ConsumeContext<BasketCreatedEvent> context)
    {
        var basketId = context.Message.BasketId;
        Console.WriteLine($"Basket created: {basketId}");
        await Task.CompletedTask;
    }
}