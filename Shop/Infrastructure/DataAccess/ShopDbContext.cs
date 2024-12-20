using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;
using Shop.Domain.Events;

namespace Shop.Infrastructure.DataAccess;

public class ShopDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Basket> Baskets { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Discount> Discounts { get; set; }

    public ShopDbContext(DbContextOptions<ShopDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Ignore<DomainEvent>();
        
        modelBuilder.Entity<Product>()
            .OwnsOne(p => p.Stock, p => { p.Property(x => x.Quantity).HasColumnName("Stock").IsRequired(); });
        
        modelBuilder.Entity<Product>()
            .OwnsOne(p => p.Price, p => { p.Property(x => x.Value).HasColumnName("Price").IsRequired(); });
        
        modelBuilder.Entity<BasketItem>()
            .HasKey(b => b.Id);

        modelBuilder.Entity<Basket>()
            .HasMany(b => b.Items)
            .WithOne()
            .HasForeignKey("BasketId");
        
        modelBuilder.Entity<BasketItem>()
            .OwnsOne(p => p.Price, p => { p.Property(x => x.Value).HasColumnName("Price").IsRequired(); });

        modelBuilder.Entity<Basket>()
            .HasOne(b => b.Discount)
            .WithOne()
            .HasForeignKey<Basket>(b => b.DiscountId);

        modelBuilder.Entity<Discount>()
            .Property(d => d.Title)
            .HasMaxLength(100)
            .IsRequired();
        
        modelBuilder.Entity<Discount>()
            .HasOne(d => d.Customer)
            .WithMany() 
            .HasForeignKey(d => d.CustomerId)
            .IsRequired();
        
        modelBuilder.Entity<Discount>()
            .Property(d => d.Status)
            .HasConversion<string>()
            .IsRequired();
        
        modelBuilder.Entity<Customer>()
            .Property(c => c.FirstName)
            .HasMaxLength(100)
            .IsRequired();
        
        modelBuilder.Entity<Basket>()
            .Property(b => b.Status)
            .HasConversion<string>()
            .IsRequired();
    }
}