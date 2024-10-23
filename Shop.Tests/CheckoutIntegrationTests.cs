using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Shop.Application.Services;
using Shop.Domain.Entities;
using Shop.Domain.Enums;
using Shop.Domain.Events;
using Shop.Infrastructure.Contracts;
using Shop.Infrastructure.DataAccess;
using Shop.Infrastructure.Repositories;

namespace Shop.Tests;

public class CheckoutIntegrationTests
{
    private readonly ShopDbContext _dbContext;
    private readonly BasketService _basketService;
    private readonly ProductService _productService;
    private readonly CustomerService _customerService;
    private readonly Mock<IDomainEventDispatcher> _mockDispatcher;

    public CheckoutIntegrationTests()
    {
        // Mock the Domain Event Dispatcher
        _mockDispatcher = new Mock<IDomainEventDispatcher>();

        // Use InMemory provider to simulate the database
        var options = new DbContextOptionsBuilder<ShopDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // unique database for each test
            .Options;

        _dbContext = new ShopDbContext(options);

        // Set up repositories and services for integration testing
        var basketRepository = new BasketRepository(_dbContext, _mockDispatcher.Object);
        var productRepository = new ProductRepository(_dbContext, _mockDispatcher.Object);
        var customerRepository = new CustomerRepository(_dbContext, _mockDispatcher.Object);
        var discountRepository = new DiscountRepository(_dbContext);

        // Set up services
        _productService = new ProductService(productRepository);
        var discountService = new DiscountService(discountRepository);
        _customerService = new CustomerService(customerRepository);
        _basketService = new BasketService(basketRepository, _productService, discountRepository, discountService);
    }

    [Fact]
    public async Task CheckoutFlow_Should_Create_Order_From_Basket()
    {
        // 1. Add Product
        await _productService.CreateProductAsync("Test Link", "Test Product", "Description", 50m, 10);
        await _dbContext.SaveChangesAsync(); // Save product to the in-memory DB
        var product = await _dbContext.Products.FirstAsync();

        // 2. Create a Customer (this might be tracked elsewhere)
        await _customerService.CreateCustomerAsync("Test First Name");
        await _dbContext.SaveChangesAsync(); // Save customer to the in-memory DB
        var customer = await _dbContext.Customers.FirstAsync();

        // 3. Create a Basket for the Customer
        await _basketService.CreateBasketAsync(customer.Id);
        await _dbContext.SaveChangesAsync();
        var basket = await _dbContext.Baskets.FirstAsync();
        
        await _basketService.AddItemToBasketAsync(basket.Id, product.Id, 2); // Add 2 units of product
        await _dbContext.SaveChangesAsync(); // Save basket and stock changes to DB

        // 4. Checkout the Basket
        await _basketService.CompletePurchase(basket.Id, customer.Id);
        await _dbContext.SaveChangesAsync(); // Finalize checkout transaction

        // 5. Verify the Results

        // Ensure the product stock has decreased
        var updatedProduct = await _dbContext.Products.FindAsync(product.Id);
        updatedProduct?.Stock.Quantity.Should().Be(8); // 10 - 2 = 8

        // Ensure the basket is marked as completed
        var completedBasket = await _dbContext.Baskets.FindAsync(basket.Id);
        completedBasket?.Status.Should().Be(BasketStatus.Completed);

        // Ensure the order is created with the correct total price
        var order = await _dbContext.Baskets.FirstOrDefaultAsync(o => o.CustomerId == customer.Id);
        order.Should().NotBeNull();
        order?.TotalPrice.Value.Should().Be(100m); // 50 * 2 = 100

        // Verify that the domain event was dispatched
        _mockDispatcher.Verify(x => x.Dispatch(It.IsAny<DomainEvent>()), Times.AtLeastOnce);
    }
}