using Shop.Application.Contracts;
using Shop.Domain.Contracts;
using Shop.Domain.Entities;
using Shop.Domain.ValueObjects;

namespace Shop.Application.Services;

public class OrderService(IOrderRepository orderRepository) : IOrderService
{
    public async Task CreateOrderAsync(Basket? basket, Price? finalPrice)
    {
        if (basket == null)
        {
            throw new ArgumentException("Basket not found.");
        }
        
        if (finalPrice == null)
        {
            throw new ArgumentException("FinalPrice is Null.");
        }
        
        var order = new Order(basket.CustomerId, basket.Items, finalPrice);

        await orderRepository.SaveAsync(order);
    }
}