using Shop.Domain.Events;
using Shop.Domain.Events.Basket;

namespace Shop.Domain.Entities;

public class Basket
{
    public Guid Id { get; private set; }
    public List<BasketItem> Items { get; private set; } = [];
    public Guid? DiscountId { get; private set; }
    public Discount? Discount { get; private set; }
    public Guid CustomerId { get; private set; } 
    
    private readonly List<DomainEvent> _domainEvents = [];

    public Basket(Guid customerId)
    {
        CustomerId = customerId;
        AddDomainEvent(new BasketCreatedEvent(this));
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
            var newItem = new BasketItem(product, quantity);
            Items.Add(newItem);

            AddDomainEvent(new BasketItemAddedEvent(this, newItem));
        }
    }

    public void RemoveItem(Guid productId)
    {
        var item = Items.FirstOrDefault(i => i.Product.Id == productId);
        if (item != null)
        {
            Items.Remove(item);
            AddDomainEvent(new BasketItemRemovedEvent(this, item));
        }
    }

    public void ApplyDiscount(Discount discount)
    {
        Discount = discount;
        DiscountId = discount.Id;

        AddDomainEvent(new DiscountAppliedEvent(this, discount));
    }
    public decimal GetTotalPrice()
    {
        var total = Items.Sum(i => i.Price.Value * i.Quantity);
        if (Discount != null)
        {
            total -= Discount.CalculateDiscount(total);
        }
        return total;
    }
    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void AddDomainEvent(DomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}



