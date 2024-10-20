using Shop.Domain.Entities;

namespace Shop.Domain.Contracts;

public interface ICustomerRepository
{
    Task SaveAsync(Customer customer);
}