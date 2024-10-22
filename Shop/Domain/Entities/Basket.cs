using Shop.Domain.Enums;
using Shop.Domain.Events;
using Shop.Domain.Events.Basket;
using Shop.Domain.ValueObjects;

namespace Shop.Domain.Entities;

public class Basket
{
    public Guid Id { get; private set; }
    public List<BasketItem> Items { get; private set; } = [];
    public Guid? DiscountId { get; private set; }
    public Discount? Discount { get; private set; }
    public Guid CustomerId { get; private set; }
    public BasketStatus Status { get; private set; }

    private readonly List<DomainEvent> _domainEvents = [];

    public Price TotalPrice
    {
        get
        {
            var total = Items.Aggregate(new Price(0),
                (sum, item) => sum.Add(item.Product.Price.Multiply(item.Quantity)));

            if (Discount == null) return total;

            var discountAmount = total.Multiply(Discount.Value);
            total = total.Subtract(discountAmount);

            return total;
        }
    }

    public Price TotalDiscountPrice
    {
        get
        {
            if (Discount == null) return new Price(0);

            var total = Items.Aggregate(new Price(0),
                (sum, item) => sum.Add(item.Product.Price.Multiply(item.Quantity)));

            var discountAmount = total.Multiply(Discount.Value);

            return discountAmount;
        }
    }

    public Basket(Guid customerId)
    {
        CustomerId = customerId;
        Status = BasketStatus.Active;
        AddDomainEvent(new BasketCreatedEvent(this));
    }

    public void AddItem(Product product, int quantity)
    {
        if (Status != BasketStatus.Active)
            throw new InvalidOperationException("Cannot add items to a completed basket.");

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
        if (Status != BasketStatus.Active)
            throw new InvalidOperationException("Cannot add items to a completed basket.");

        var item = Items.FirstOrDefault(i => i.Product.Id == productId);
        if (item != null)
        {
            Items.Remove(item);
            AddDomainEvent(new BasketItemRemovedEvent(this, item));
        }
    }

    public void ApplyDiscount(Discount discount)
    {
        if (Status != BasketStatus.Active)
            throw new InvalidOperationException("Cannot apply discount to a completed basket.");

        Discount = discount;

        AddDomainEvent(new DiscountAppliedEvent(this, discount));
    }

    public void CompletePurchase()
    {
        if (Status != BasketStatus.Active)
            throw new InvalidOperationException("Basket is already completed.");

        Status = BasketStatus.Completed;
        AddDomainEvent(new BasketCompletedEvent(this));
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