using Shop.Domain.ValueObjects;

namespace Shop.Domain.Entities;

public class Product
{
    public Guid Id { get; private set; }
    public string Picture { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public Price Price { get; private set; } 
    
    private Product() { }

    public Product(string picture, string title, string description, Price price)
    {
        Id = Guid.NewGuid();
        Picture = picture;
        Title = title;
        Description = description;
        Price = price;
    }
}


