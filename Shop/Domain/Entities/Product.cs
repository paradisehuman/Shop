using Shop.Domain.ValueObjects;

namespace Shop.Domain.Entities;

public class Product
{
    public Guid Id { get; private set; }
    public string Picture { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public Price Price { get; private set; }
    public int StockQuantity { get; private set; } 
    
    private Product() { }

    public Product(string picture, string title, string description, Price price, int initialStock)
    {
        Id = Guid.NewGuid();
        Picture = picture;
        Title = title;
        Description = description;
        Price = price;
        StockQuantity = initialStock;
    }
    
    public void AddStock(int quantity)
    {
        if (quantity <= 0)
        {
            throw new ArgumentException("Quantity to add must be greater than zero.");
        }
        StockQuantity += quantity;
    }
    
    public void ReduceStock(int quantity)
    {
        if (quantity <= 0)
        {
            throw new ArgumentException("Quantity to reduce must be greater than zero.");
        }
        if (StockQuantity - quantity < 0)
        {
            throw new InvalidOperationException("Not enough stock available.");
        }
        StockQuantity -= quantity;
    }
    
    public bool IsInStock(int quantity)
    {
        return StockQuantity >= quantity;
    }
}


