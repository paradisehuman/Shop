using Shop.Domain.Entities;
using Shop.Domain.ValueObjects;

namespace Shop.Application.Contracts;

public interface IOrderService
{
    Task CreateOrderAsync(Basket? basket, Price? finalPrice);
}