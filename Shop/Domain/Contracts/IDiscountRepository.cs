using Shop.Domain.Entities;

namespace Shop.Domain.Contracts;

public interface IDiscountRepository
{
    Discount? GetById(Guid id);
    IEnumerable<Discount> GetByCustomerId(Guid customerId);
    Task SaveAsync(Discount discount);
}
