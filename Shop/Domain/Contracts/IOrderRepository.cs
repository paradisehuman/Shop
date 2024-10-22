using Shop.Domain.Entities;

namespace Shop.Domain.Contracts;

public interface IOrderRepository
{
    Task SaveAsync(Order order);
}
