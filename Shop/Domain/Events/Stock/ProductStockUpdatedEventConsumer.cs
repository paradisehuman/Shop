using MassTransit;

namespace Shop.Domain.Events.Stock;

public class ProductStockUpdatedEventConsumer : IConsumer<ProductStockUpdatedEvent>
{
    public async Task Consume(ConsumeContext<ProductStockUpdatedEvent> context)
    {
        var quantityChange = context.Message.QuantityChange;
        Console.WriteLine($"Product stock updated, Quantity Change: {quantityChange}");
        await Task.CompletedTask;
    }
}
