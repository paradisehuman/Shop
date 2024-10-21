using MassTransit;

namespace Shop.Domain.Events.Basket;

public class BasketItemAddedEventConsumer : IConsumer<BasketItemAddedEvent>
{
    public async Task Consume(ConsumeContext<BasketItemAddedEvent> context)
    {
        var item = context.Message.Item;
        Console.WriteLine($"Item added to basket: {item.Product.Title}, Quantity: {item.Quantity}");
        await Task.CompletedTask;
    }
}