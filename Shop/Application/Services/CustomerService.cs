using Shop.Application.Contracts;
using Shop.Domain.Contracts;
using Shop.Domain.Entities;

namespace Shop.Application.Services;

public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
{
    public async Task CreateCustomerAsync(string firstName)
    {
        var customer = new Customer(firstName);
        await customerRepository.SaveAsync(customer);
    }
    
}
