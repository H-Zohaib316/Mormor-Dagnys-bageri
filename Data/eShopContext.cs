using Microsoft.EntityFrameworkCore;
using MormorBageri.Entities;
namespace MormorBageri.Data;

//DbContextoptions (visar vart  vi har databasen)
////////////
/// ///
public class EShopContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Product> Products {get; set;}
    public DbSet<Supplier> Suppliers {get; set;}
    public DbSet<SupplierProduct> SupplierProducts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SupplierProduct>().HasKey(sp=> new{ sp.SupplierId, sp.ProductId});
        base.OnModelCreating(modelBuilder);
    }






}
