using Shop.Application.Contracts;
using Shop.Domain.Contracts;
using Shop.Domain.Entities;

namespace Shop.Application.Services;

public class CustomerService: ICustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task CreateCustomerAsync(string firstName)
    {
        var customer = new Customer(firstName);
        await _customerRepository.SaveAsync(customer);
    }
    
}
