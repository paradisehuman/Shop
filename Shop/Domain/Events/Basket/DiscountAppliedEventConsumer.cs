using MassTransit;

namespace Shop.Domain.Events.Basket;

public class DiscountAppliedEventConsumer : IConsumer<DiscountAppliedEvent>
{
    public async Task Consume(ConsumeContext<DiscountAppliedEvent> context)
    {
        var basket = context.Message.Basket;
        var discount = context.Message.Discount;
        Console.WriteLine($"Discount '{discount.Title}' applied to basket {basket.Id}");
        await Task.CompletedTask;
    }
}