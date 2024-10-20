using Shop.Domain.Contracts;

namespace Shop.Domain.Entities;

public class Basket : IAggregateRoot
{
    public Guid Id { get; private set; }
    public List<BasketItem> Items { get; private set; } = [];
    public Discount? Discount { get; private set; }
    
    public Guid? DiscountId { get; private set; }

    public Basket()
    {
        Id = Guid.NewGuid();
    }

    public void AddItem(Product product, int quantity)
    {
        var basketItem = Items.FirstOrDefault(i => i.Product.Id == product.Id);
        if (basketItem != null)
        {
            basketItem.IncreaseQuantity(quantity);
        }
        else
        {
            Items.Add(new BasketItem(product, quantity));
        }
    }

    public void ApplyDiscount(Discount discount)
    {
        Discount = discount;
    }
}


