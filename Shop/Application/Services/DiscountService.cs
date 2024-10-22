using Shop.Application.Contracts;
using Shop.Domain.Contracts;
using Shop.Domain.Entities;
using Shop.Domain.ValueObjects;

namespace Shop.Application.Services;

public class DiscountService(IDiscountRepository discountRepository) : IDiscountService
{
    public async Task ApplyDiscountForNextPurchase(Guid customerId)
    {
        var discount = new Discount(0.05m, "5% off next purchase", customerId);
        await discountRepository.SaveAsync(discount);
    }
}