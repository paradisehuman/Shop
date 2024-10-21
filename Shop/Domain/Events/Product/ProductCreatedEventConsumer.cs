using MassTransit;

namespace Shop.Domain.Events.Product;

public class ProductCreatedEventConsumer : IConsumer<ProductCreatedEvent>
{
    public async Task Consume(ConsumeContext<ProductCreatedEvent> context)
    {
        var product = context.Message.Product;
        Console.WriteLine($"Product created: {product.Title}");
        await Task.CompletedTask;
    }
}