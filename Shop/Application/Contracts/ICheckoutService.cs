namespace Shop.Application.Contracts;

public interface ICheckoutService
{
    Task CheckoutAsync(Guid basketId, Guid customerId);
}