using Shop.Application.Contracts;
using Shop.Domain.Contracts;
using Shop.Domain.Entities;

namespace Shop.Application.Services;

public class BasketService(
    IBasketRepository basketRepository,
    IProductRepository productRepository,
    IDiscountRepository discountRepository,
    IDiscountService discountService)
    : IBasketService
{
    public async Task CreateBasketAsync(Guid customerId)
    {
        var basket = new Basket(customerId);
        await basketRepository.SaveAsync(basket);
    }

    public async Task AddItemToBasketAsync(Guid basketId, Guid productId, int quantity)
    {
        var basket = basketRepository.GetById(basketId);
        if (basket == null) throw new ArgumentException("Basket not found.");

        var product = productRepository.GetById(productId);
        if (product == null) throw new ArgumentException("Product not found.");

        basket.AddItem(product, quantity);
        await basketRepository.SaveAsync(basket);
    }

    public async Task RemoveItemFromBasketAsync(Guid basketId, Guid productId)
    {
        var basket = basketRepository.GetById(basketId);
        if (basket == null) throw new ArgumentException("Basket not found.");

        basket.RemoveItem(productId);
        await basketRepository.SaveAsync(basket);
    }

    public async Task ApplyDiscountToBasket(Guid basketId, Guid customerId)
    {
        var basket = basketRepository.GetById(basketId);
        if (basket == null)
        {
            throw new ArgumentException("Basket not found.");
        }

        var discount = discountRepository.GetByCustomerId(customerId)
            .FirstOrDefault();

        if (discount != null)
        {
            basket?.ApplyDiscount(discount);

            await basketRepository.SaveAsync(basket!);
            await discountRepository.SaveAsync(discount);
        }
    }

    public async Task CompletePurchase(Guid basketId, Guid customerId)
    {
        var basket = basketRepository.GetById(basketId);
        if (basket == null)
        {
            throw new ArgumentException("Basket not found.");
        }

        basket.CompletePurchase();
        await basketRepository.SaveAsync(basket);

        await discountService.ApplyDiscountForNextPurchase(customerId);
    }

    public Basket? GetBasket(Guid basketId)
    {
        return basketRepository.GetById(basketId);
    }

    public decimal GetBasketTotalPrice(Guid basketId)
    {
        var basket = basketRepository.GetById(basketId);
        if (basket == null)
        {
            throw new ArgumentException("Basket not found.");
        }

        var finalPrice = basket.TotalPrice.Value;
        return finalPrice;
    }

    public decimal GetAllDiscountPrices()
    {
        var baskets = basketRepository.GetCompletedBaskets();
        var allDiscountPrices = baskets.Sum(basket => basket.TotalDiscountPrice.Value);
        return allDiscountPrices;
    }
}