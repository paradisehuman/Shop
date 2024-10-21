using MassTransit;

namespace Shop.Domain.Events.Product;

public class ProductStockUpdatedEventConsumer : IConsumer<ProductStockUpdatedEvent>
{
    public async Task Consume(ConsumeContext<ProductStockUpdatedEvent> context)
    {
        var product = context.Message.Product;
        var quantityChange = context.Message.QuantityChange;
        Console.WriteLine($"Product stock updated: {product.Title}, Quantity Change: {quantityChange}");
        await Task.CompletedTask;
    }
}
