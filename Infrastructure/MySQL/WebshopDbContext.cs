using Microsoft.EntityFrameworkCore;

namespace Infrastructure.MySQL;

using Domain;

public class WebshopDbContext : DbContext
{
    
    public DbSet<Address> Address { get; set; }
    public DbSet<Customer> Customer { get; set; }
    public DbSet<Order> Order { get; set; }
    public DbSet<OrderProduct> Order_Product { get; set; }
    public DbSet<Product> Product { get; set; }

    public WebshopDbContext(DbContextOptions<WebshopDbContext> contextOptions): base(contextOptions)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Customer>()
            .HasOne(c => c.Address)
            .WithMany(a => a.Customers)
            .HasForeignKey(c => c.AddressId);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.Customer)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.CustomerId);

        modelBuilder.Entity<OrderProduct>()
            .HasKey(op => new { op.ProductId, op.OrderId });

        modelBuilder.Entity<OrderProduct>()
            .HasOne(po => po.Product)
            .WithMany(p => p.OrderProducts)
            .HasForeignKey(po => po.ProductId);

        modelBuilder.Entity<OrderProduct>()
            .HasOne(po => po.Order)
            .WithMany(o => o.OrderProducts)
            .HasForeignKey(po => po.OrderId);

        modelBuilder.Entity<Customer>().HasData(
            new Customer
            {
                ID = 1,
                Name = "John Doe",
                Email = "johndoe@example.com",
                AddressId = 1
            },
            new Customer
            {
                ID = 2,
                Name = "Jane Smith",
                Email = "janesmith@example.com",
                AddressId = 2
            }
        );

        modelBuilder.Entity<Address>().HasData(
            new Address
            {
                ID = 1,
                City = "New York",
                PostalCode = "10001",
                Street = "123 Main St",
                Country = "USA"
            },
            new Address
            {
                ID = 2,
                City = "San Francisco",
                PostalCode = "94105",
                Street = "456 Main St",
                Country = "USA"
            }
        );

        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                ID = 1,
                Name = "Product 1",
                Description = "This is product 1",
                Price = 10.99m,
                Stock = 100
            },
            new Product
            {
                ID = 2,
                Name = "Product 2",
                Description = "This is product 2",
                Price = 15.99m,
                Stock = 50
            },
            new Product
            {
                ID = 3,
                Name = "Product 3",
                Description = "This is product 3",
                Price = 8.99m,
                Stock = 200
            },
            new Product
            {
                ID = 4,
                Name = "Product 4",
                Description = "This is product 4",
                Price = 20.99m,
                Stock = 75
            }
        );

        modelBuilder.Entity<Order>().HasData(
            new Order
            {
                ID = 1,
                OrderId = Guid.NewGuid(),
                Psp = PSP.CREDITCARD,
                CustomerId = 1
            },
            new Order
            {
                ID = 2,
                OrderId = Guid.NewGuid(),
                Psp = PSP.PAYPAL,
                CustomerId = 2
            },
            new Order
            {
                ID = 3,
                OrderId = Guid.NewGuid(),
                Psp = PSP.IDEAL,
                CustomerId = 2
            }
        );

        modelBuilder.Entity<OrderProduct>().HasData(
            new OrderProduct
            {
                ID = 1,
                OrderId = 1,
                ProductId = 1
            },
            new OrderProduct
            {
                ID = 2,
                OrderId = 1,
                ProductId = 2
            },
            new OrderProduct
            {
                ID = 3,
                OrderId = 2,
                ProductId = 1
            });
    }
}