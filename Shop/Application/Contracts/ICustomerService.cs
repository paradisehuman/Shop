namespace Shop.Application.Contracts;

public interface ICustomerService
{
    Task CreateCustomerAsync(string firstName);
}