using Microsoft.EntityFrameworkCore;
using Shop.Domain.Contracts;
using Shop.Domain.Entities;
using Shop.Infrastructure.Contracts;
using Shop.Infrastructure.DataAccess;

namespace Shop.Infrastructure.Repositories;

public class BasketRepository : IBasketRepository
{
    private readonly ShopDbContext _dbContext;
    private readonly IDomainEventDispatcher _dispatcher;

    public BasketRepository(ShopDbContext dbContext, IDomainEventDispatcher dispatcher)
    {
        _dbContext = dbContext;
        _dispatcher = dispatcher;
    }

    public Basket? GetById(Guid id)
    {
        return _dbContext.Baskets.Include(b => b.Items).ThenInclude(i => i.Product).FirstOrDefault(b => b.Id == id);
    }

    public async Task SaveAsync(Basket basket)
    {
        if (_dbContext.Entry(basket).State == EntityState.Detached)
        {
            _dbContext.Baskets.Add(basket);
        }
        else
        {
            _dbContext.Baskets.Update(basket);
        }

        foreach (var domainEvent in basket.DomainEvents)
        {
            await _dispatcher.Dispatch(domainEvent);
        }

        basket.ClearDomainEvents();
        await _dbContext.SaveChangesAsync();
    }
}