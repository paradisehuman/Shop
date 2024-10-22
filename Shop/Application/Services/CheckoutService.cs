using Shop.Application.Contracts;
using Shop.Domain.Enums;

namespace Shop.Application.Services;

public class CheckoutService(IBasketService basketService, IOrderService orderService) : ICheckoutService
{
    public async Task CheckoutAsync(Guid basketId, Guid customerId)
    {
        var basket = basketService.GetBasket(basketId);
   
        if (basket?.Status != BasketStatus.Active)
        {
            throw new InvalidOperationException("Cannot complete a purchase for a non-active basket.");
        }
        
        await basketService.ApplyDiscountToBasket(basketId, customerId);
        
        var finalPrice = basket?.TotalPrice;
        
        await orderService.CreateOrderAsync(basket, finalPrice);
        
        await basketService.CompletePurchase(basketId, customerId);
    }
}