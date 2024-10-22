using Shop.Domain.ValueObjects;

namespace Shop.Domain.Entities;

public class Order
{
    public Guid Id { get; private set; }
    public Guid CustomerId { get; private set; }
    public List<BasketItem> Items { get; private set; } = [];
    public Price TotalPrice { get; private set; }
    
    private Order() { }

    public Order(Guid customerId, List<BasketItem> items, Price totalPrice)
    {
        CustomerId = customerId;
        Items = items;
        TotalPrice = totalPrice;
    }
}
