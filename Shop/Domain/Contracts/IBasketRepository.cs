using Shop.Domain.Entities;

namespace Shop.Domain.Contracts;

public interface IBasketRepository
{
    Basket? GetById(Guid id);
    IEnumerable<Basket> GetCompletedBaskets();
    Task SaveAsync(Basket basket);
}
