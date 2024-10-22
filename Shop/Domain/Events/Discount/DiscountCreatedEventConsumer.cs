using MassTransit;

namespace Shop.Domain.Events.Discount;

public class DiscountCreatedEventConsumer : IConsumer<DiscountCreatedEvent>
{
    public async Task Consume(ConsumeContext<DiscountCreatedEvent> context)
    {
        var discount = context.Message.Discount;
        Console.WriteLine($"New discount created for customer {discount.CustomerId}: {discount.Title}, Value: {discount.Value * 100}%");

        await Task.CompletedTask;
    }
}