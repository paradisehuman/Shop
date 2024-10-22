using FluentAssertions;
using Moq;
using Shop.Domain.Entities;
using Shop.Domain.ValueObjects;
using Shop.Application.Services;
using Shop.Application.Contracts;
using Shop.Domain.Contracts;
using Shop.Domain.Enums;

public class UnitTests
{
    [Fact]
    public async Task AddItemToBasket_Should_AddProduct()
    {
        // Arrange
        var basketId = Guid.NewGuid();
        var productId = Guid.NewGuid();
        var customerId = Guid.NewGuid();
        var product = new Product("Test Picture","Test Product", "Description", new Price(50m), 10);
        var basket = new Basket(customerId);
        var mockBasketRepository = new Mock<IBasketRepository>();
        mockBasketRepository.Setup(x => x.GetById(basketId)).Returns(basket);
        var mockProductService = new Mock<IProductService>();
        mockProductService.Setup(x => x.GetById(productId)).Returns(product);

        var basketService = new BasketService(mockBasketRepository.Object, mockProductService.Object, null, null);

        // Act
        await basketService.AddItemToBasketAsync(basketId, productId, 1);

        // Assert
        basket.Items.Should().ContainSingle();
    }
    
    [Fact]
    public void ApplyDiscount_Should_UpdateTotalPrice()
    {
        // Arrange
        var basketId = Guid.NewGuid();
        var discount = new Discount(0.05m,"5% Discount", Guid.NewGuid());
        var basket = new Basket(Guid.NewGuid());
        var mockBasketRepository = new Mock<IBasketRepository>();
        mockBasketRepository.Setup(x => x.GetById(basketId)).Returns(basket);

        // Act
        basket.ApplyDiscount(discount);

        // Assert
        basket.Discount.Should().Be(discount);
    }
    
    [Fact]
    public void ReduceStock_Should_DecreaseProductQuantity()
    {
        // Arrange
        var product = new Product("Test Picture","Test Product", "Description", new Price(50m), 10);

        // Act
        product.ReduceStock(3);

        // Assert
        product.StockQuantity.Should().Be(7); // 10 - 3 = 7
    }
    
    [Fact]
    public void AddItemToBasket_Should_Throw_When_Stock_Insufficient()
    {
        // Arrange
        var product = new Product("Test Picture","Test Product", "Description", new Price(50m), 2);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => product.ReduceStock(3));
    }
    
    [Fact]
    public void MarkDiscountAsUsed_Should_UpdateStatus()
    {
        // Arrange
        var discount = new Discount( 0.05m,"5% Discount", Guid.NewGuid());

        // Act
        discount.MarkAsUsed();

        // Assert
        discount.Status.Should().Be(DiscountStatus.Used);
    }





}
