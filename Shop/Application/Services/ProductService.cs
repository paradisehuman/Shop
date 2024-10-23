using Shop.Application.Contracts;
using Shop.Domain.Contracts;
using Shop.Domain.Entities;
using Shop.Domain.ValueObjects;

namespace Shop.Application.Services;

public class ProductService(IProductRepository productRepository) : IProductService
{
    public async Task CreateProductAsync(string picture, string title, string description, decimal price,
        int initialStock)
    {
        var product = new Product(picture, title, description, new Price(price), new Stock(initialStock));
        await productRepository.SaveAsync(product);
    }

    public async Task AddStockAsync(Guid productId, int quantity)
    {
        var product = productRepository.GetById(productId);
        if (product == null)
        {
            throw new ArgumentException("Product not found.");
        }

        product.Stock.IncreaseStock(quantity);
        await productRepository.SaveAsync(product);
    }

    public async Task ReduceStockAsync(Guid productId, int quantity)
    {
        var product = productRepository.GetById(productId);
        if (product == null)
        {
            throw new ArgumentException("Product not found.");
        }

        product.Stock.ReduceStock(quantity);
        await productRepository.SaveAsync(product);
    }

    public Product? GetById(Guid id)
    {
        return productRepository.GetById(id);
    }
}