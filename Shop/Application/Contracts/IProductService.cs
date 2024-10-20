using Shop.Domain.ValueObjects;

namespace Shop.Application.Contracts;

public interface IProductService
{
    Task CreateProductAsync(string picture, string title, string description, decimal price, int initialStock);
    Task AddStockAsync(Guid productId, int quantity);
    Task ReduceStockAsync(Guid productId, int quantity);
}