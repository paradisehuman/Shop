using MassTransit;

namespace Shop.Domain.Events.Basket;

public class BasketCompletedEventConsumer : IConsumer<BasketCompletedEvent>
{
    public async Task Consume(ConsumeContext<BasketCompletedEvent> context)
    {
        var basket = context.Message.Basket;
        Console.WriteLine($"Basket {basket.Id} for customer {basket.CustomerId} has been completed.");
        
        await Task.CompletedTask;
    }
}