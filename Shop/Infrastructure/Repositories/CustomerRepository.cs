using Shop.Domain.Contracts;
using Shop.Domain.Entities;
using Shop.Infrastructure.Contracts;
using Shop.Infrastructure.DataAccess;

namespace Shop.Infrastructure.Repositories;

public class CustomerRepository(ShopDbContext dbContext, IDomainEventDispatcher dispatcher) : ICustomerRepository
{
    public async Task SaveAsync(Customer customer)
    {
        dbContext.Customers.Add(customer);
        
        foreach (var domainEvent in customer.DomainEvents)
        {
            await dispatcher.Dispatch(domainEvent);
        }

        customer.ClearDomainEvents();
        
        await dbContext.SaveChangesAsync();
    }
}