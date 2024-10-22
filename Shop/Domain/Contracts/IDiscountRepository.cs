using Shop.Domain.Entities;

namespace Shop.Domain.Contracts;

public interface IDiscountRepository
{
    Discount? GetActiveByCustomerId(Guid customerId);
    Task SaveAsync(Discount discount);
}
