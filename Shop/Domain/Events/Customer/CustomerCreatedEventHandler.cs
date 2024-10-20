using MassTransit;

namespace Shop.Domain.Events.Customer;

public class CustomerCreatedEventHandler : IConsumer<CustomerCreatedEvent>
{
    public async Task Consume(ConsumeContext<CustomerCreatedEvent> context)
    {
        var customer = context.Message.Customer;
        Console.WriteLine($"New customer created: {customer.FirstName}");
        await Task.CompletedTask;
    }
}