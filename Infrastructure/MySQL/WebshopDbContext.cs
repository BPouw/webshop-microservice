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
    }
}