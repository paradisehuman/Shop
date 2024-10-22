using Shop.Domain.Entities;

namespace Shop.Application.Contracts;

public interface IBasketService
{
    Task CreateBasketAsync(Guid customerId);
    Task AddItemToBasketAsync(Guid basketId, Guid productId, int quantity);
    Task RemoveItemFromBasketAsync(Guid basketId, Guid productId);
    Task ApplyDiscountToBasket(Guid basketId, Guid customerId);
    Task CompletePurchase(Guid basketId, Guid customerId);
    Basket? GetBasket(Guid basketId);
    decimal GetBasketTotalPrice(Guid basketId);
    decimal GetAllDiscountPrices();
}