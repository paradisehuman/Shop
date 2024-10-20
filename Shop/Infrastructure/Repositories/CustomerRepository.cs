using Shop.Domain.Contracts;
using Shop.Domain.Entities;
using Shop.Infrastructure.Contracts;
using Shop.Infrastructure.DataAccess;

namespace Shop.Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly ShopDbContext _dbContext;
    private readonly IDomainEventDispatcher _dispatcher;

    public CustomerRepository(ShopDbContext dbContext, IDomainEventDispatcher dispatcher)
    {
        _dbContext = dbContext;
        _dispatcher = dispatcher;
    }

    public async Task SaveAsync(Customer customer)
    {
        _dbContext.Customers.Add(customer);
        
        foreach (var domainEvent in customer.DomainEvents)
        {
            await _dispatcher.Dispatch(domainEvent);
        }

        customer.ClearDomainEvents();
        
        await _dbContext.SaveChangesAsync();
    }
}