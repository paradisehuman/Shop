using Microsoft.EntityFrameworkCore;
using Shop.Domain.Contracts;
using Shop.Domain.Entities;
using Shop.Domain.Enums;
using Shop.Infrastructure.DataAccess;

namespace Shop.Infrastructure.Repositories;

public class DiscountRepository : IDiscountRepository
{
    private readonly ShopDbContext _dbContext;

    public DiscountRepository(ShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Discount? GetActiveByCustomerId(Guid customerId)
    {
        return _dbContext.Discounts.FirstOrDefault(d => d.CustomerId == customerId && d.Status == DiscountStatus.Active);
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
