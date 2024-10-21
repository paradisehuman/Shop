using Microsoft.EntityFrameworkCore;
using Shop.Domain.Contracts;
using Shop.Domain.Entities;
using Shop.Infrastructure.Contracts;
using Shop.Infrastructure.DataAccess;

namespace Shop.Infrastructure.Repositories;

public class ProductRepository(ShopDbContext dbContext, IDomainEventDispatcher dispatcher) : IProductRepository
{
    public async Task SaveAsync(Product product)
    {
        if (dbContext.Entry(product).State == EntityState.Detached)
        {
            dbContext.Products.Add(product);
        }
        else
        {
            dbContext.Products.Update(product);
        }
        
        foreach (var domainEvent in product.DomainEvents)
        {
            await dispatcher.Dispatch(domainEvent);
        }

        product.ClearDomainEvents();
        await dbContext.SaveChangesAsync();
    }
    
    public Product? GetById(Guid id)
    {
        return dbContext.Products.Find(id);
    }
}
