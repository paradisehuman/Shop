namespace Shop.Application.Contracts;

public interface IDiscountService
{
    Task ApplyDiscountForNextPurchase(Guid customerId);
}