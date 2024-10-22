using Shop.Domain.Contracts;
using Shop.Domain.Entities;
using Shop.Infrastructure.DataAccess;

namespace Shop.Infrastructure.Repositories;

public class OrderRepository(ShopDbContext dbContext) : IOrderRepository
{
    public Task SaveAsync(Order order)
    {
        return Task.CompletedTask;
    }
}