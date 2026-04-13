using MormorBageri.Entities;
using Microsoft.EntityFrameworkCore;

namespace MormorBageri.Data;

public class DbSeedData
{
     public static async Task SeedAsync(EShopContext context)
    {
        if (await context.Suppliers.AnyAsync() ||
            await context.Products.AnyAsync() ||
            await context.SupplierProducts.AnyAsync())
        {
            return;
        }

       
    var willys = new Supplier
{
    SupplierName = "Willys",
    Address = "Storgatan 1",
    ContactPerson = "Anna Svensson",
    Phone = "070-111 22 33",
    Email = "willys@test.se"
};

var icA = new Supplier
{
    SupplierName = "ICA",
    Address = "Köpmansgatan 5",
    ContactPerson = "Erik Johansson",
    Phone = "070-222 33 44",
    Email = "ica@test.se"
};

       
        var mjol = new Product
        {
            ItemNumber = "MJOL-001",
            ProductName = "Mjöl"
        };

        var socker = new Product
        {
            ItemNumber = "SOCKER-001",
            ProductName = "Socker"
        };

        var smor = new Product
        {
            ItemNumber = "SMOR-001",
            ProductName = "Smör"
        };

        context.Suppliers.AddRange(willys, icA);
        context.Products.AddRange(mjol, socker, smor);

        await context.SaveChangesAsync();

    
        var relations = new List<SupplierProduct>
        {
            new() {
                SupplierId = willys.SupplierId,
                ProductId = mjol.Id,
                PricePerKg = 12.50m
            },
            new() {
                SupplierId = willys.SupplierId,
                ProductId = socker.Id,
                PricePerKg = 14.90m
            },
            new() {
                SupplierId = icA.SupplierId,
                ProductId = mjol.Id,
                PricePerKg = 11.80m
            },
            new() {
                SupplierId = icA.SupplierId,
                ProductId = smor.Id,
                PricePerKg = 45.00m
            }
        };

        context.SupplierProducts.AddRange(relations);

        await context.SaveChangesAsync();
    }
}


