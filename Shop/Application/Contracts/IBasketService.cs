using Shop.Domain.Entities;

namespace Shop.Application.Contracts;

public interface IBasketService
{
    Task CreateBasketAsync(Guid customerId);
    Task AddItemToBasketAsync(Guid basketId, Guid productId, int quantity);
    Task RemoveItemFromBasketAsync(Guid basketId, Guid productId);
    Task ApplyDiscountAsync(Guid basketId, Discount discount);
}