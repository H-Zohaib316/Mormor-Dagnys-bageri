using Microsoft.EntityFrameworkCore;
using MormorBageri.Entities;
namespace MormorBageri.Data;

//DbContextoptions (visar vart  vi har databasen)
////////////
/// ///
public class EShopContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Customer> Customers {get; set;}
    public DbSet<Product> Products {get; set;}
    public DbSet<SalesOrder> SalesOrders {get; set;}
    public DbSet<OrderItem> OrderItems {get; set;}
    public DbSet<Supplier> Suppliers {get; set;}
    public DbSet<SupplierProduct> SupplierProducts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SupplierProduct>().HasKey(sp=> new{ sp.SupplierId, sp.ProductId});
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<SupplierProduct>()
            .HasOne(sp => sp.Supplier)
            .WithMany(s => s.SupplierProducts)
            .HasForeignKey(sp => sp.SupplierId);

        modelBuilder.Entity<SupplierProduct>()
            .HasOne(sp => sp.Product)
            .WithMany(p => p.SupplierProducts)
            .HasForeignKey(sp => sp.ProductId);

        modelBuilder.Entity<SalesOrder>()
            .HasOne(o => o.Customer)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.CustomerId);

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.SalesOrder)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.Id);

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Product)
            .WithMany(bp => bp.OrderItems)
            .HasForeignKey(oi => oi.ProductId);

    }

    






}
