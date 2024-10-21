using Shop.Domain.Entities;

namespace Shop.Domain.Contracts;

public interface IBasketRepository
{
    Basket? GetById(Guid id);
    Task SaveAsync(Basket basket);
}
