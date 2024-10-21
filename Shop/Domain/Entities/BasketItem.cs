using Shop.Domain.ValueObjects;

namespace Shop.Domain.Entities;

public class BasketItem
{
    public Guid Id { get; private set; }
    public Product Product { get; private set; }
    public int Quantity { get; private set; }
    public Price Price { get; private set; }

    private BasketItem() { }

    public BasketItem(Product product, int quantity)
    {
        Product = product;
        Quantity = quantity;
        Price = product.Price;
    }

    public void IncreaseQuantity(int amount)
    {
        Quantity += amount;
    }

    public void DecreaseQuantity(int amount)
    {
        if (Quantity - amount > 0)
        {
            Quantity -= amount;
        }
        else
        {
            throw new InvalidOperationException("Cannot decrease quantity below zero.");
        }
    }
}

