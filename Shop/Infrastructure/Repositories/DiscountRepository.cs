using Microsoft.EntityFrameworkCore;
using Shop.Domain.Contracts;
using Shop.Domain.Entities;
using Shop.Infrastructure.DataAccess;

namespace Shop.Infrastructure.Repositories;

public class DiscountRepository : IDiscountRepository
{
    private readonly ShopDbContext _dbContext;

    public DiscountRepository(ShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Discount? GetById(Guid id)
    {
        return _dbContext.Discounts.Find(id);
    }

    public IEnumerable<Discount> GetByCustomerId(Guid customerId)
    {
        return _dbContext.Discounts.Where(d => d.CustomerId == customerId).ToList();
    }

    public async Task SaveAsync(Discount discount)
    {
        if (_dbContext.Entry(discount).State == EntityState.Detached)
        {
            _dbContext.Discounts.Add(discount);
        }
        else
        {
            _dbContext.Discounts.Update(discount);
        }
        
        discount.ClearDomainEvents();

        await _dbContext.SaveChangesAsync();
    }
}
