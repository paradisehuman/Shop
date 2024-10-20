namespace Shop.Domain.Entities;

public class BasketItem
{
    public Guid Id { get; private set; }
    public Product Product { get; private set; }
    public int Quantity { get; private set; }

    private BasketItem() { }

    public BasketItem(Product product, int quantity)
    {
        Id = Guid.NewGuid();
        Product = product;
        Quantity = quantity;
    }

    public void IncreaseQuantity(int amount)
    {
        Quantity += amount;
    }
}
