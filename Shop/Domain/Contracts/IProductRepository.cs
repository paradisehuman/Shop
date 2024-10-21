using Shop.Domain.Entities;

namespace Shop.Domain.Contracts;

public interface IProductRepository
{
    Task SaveAsync(Product product);
    Product? GetById(Guid id);
}
