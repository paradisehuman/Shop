using MassTransit;

namespace Shop.Domain.Events.Basket;

public class BasketItemRemovedEventConsumer : IConsumer<BasketItemRemovedEvent>
{
    public async Task Consume(ConsumeContext<BasketItemRemovedEvent> context)
    {
        var item = context.Message.Item;
        Console.WriteLine($"Item removed from basket: {item.Product.Title}");
        await Task.CompletedTask;
    }
}