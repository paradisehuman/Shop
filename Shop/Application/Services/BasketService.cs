using Shop.Application.Contracts;
using Shop.Domain.Contracts;
using Shop.Domain.Entities;

namespace Shop.Application.Services;

public class BasketService: IBasketService
{
    private readonly IBasketRepository _basketRepository;
    private readonly IProductRepository _productRepository;

    public BasketService(IBasketRepository basketRepository, IProductRepository productRepository)
    {
        _basketRepository = basketRepository;
        _productRepository = productRepository;
    }
    
    public async Task CreateBasketAsync(Guid customerId)
    {
        var basket = new Basket(customerId);
        await _basketRepository.SaveAsync(basket);
    }

    public async Task AddItemToBasketAsync(Guid basketId, Guid productId, int quantity)
    {
        var basket = _basketRepository.GetById(basketId);
        if (basket == null) throw new ArgumentException("Basket not found.");

        var product = _productRepository.GetById(productId);
        if (product == null) throw new ArgumentException("Product not found.");

        basket.AddItem(product, quantity);
        await _basketRepository.SaveAsync(basket);
    }

    public async Task RemoveItemFromBasketAsync(Guid basketId, Guid productId)
    {
        var basket = _basketRepository.GetById(basketId);
        if (basket == null) throw new ArgumentException("Basket not found.");

        basket.RemoveItem(productId);
        await _basketRepository.SaveAsync(basket);
    }

    public async Task ApplyDiscountAsync(Guid basketId, Discount discount)
    {
        var basket = _basketRepository.GetById(basketId);
        if (basket == null) throw new ArgumentException("Basket not found.");

        basket.ApplyDiscount(discount);
        await _basketRepository.SaveAsync(basket);
    }
}