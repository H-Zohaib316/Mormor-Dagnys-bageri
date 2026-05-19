using Microsoft.EntityFrameworkCore;
using MormorBageri.Data;
using MormorBageri.DTOs.Products;
using MormorBageri.Entities;
using MormorBageri.interfaces;

namespace MormorBageri.repositories;

public class ProductRepository(EShopContext context) : IProductRepository
{
    public async Task<bool> AddProduct(PostProductDto product)
    {
        try
        {
            
            Product item = new()
            {
                ProductName = product.ProductName,
                PricePerPiece = product.PricePerPiece,
                Weight = product.Weight,
                PackageAmount = product.PackageAmount,
                BestBeforeDate = product.BestBeforeDate,
                ManufacturedDate = product.ManufacturedDate,
      
            };

            context.Products.Add(item);

            return true;
        }
        catch (Exception ex)
        {
            
            throw new Exception(ex.Message);
        }
    }

    public async Task<GetProductDto> FindProduct(int id)
    {
        try
        {
        var product = await context.Products
            .Where(p=> p.Id == id)
            .Select(p=> new GetProductDto
            {
                Id = p.Id,
                ProductName = p.ProductName,
                PricePerPiece = p.PricePerPiece,
                Weight = p.Weight,
                PackageAmount = p.PackageAmount,
                BestBeforeDate = p.BestBeforeDate,
                
            
            }).SingleOrDefaultAsync() ?? throw new Exception("Hittade ingen Produkt");

            return product;
            
        }
        catch (Exception ex)
        {
            
            throw new Exception(ex.Message);
        }
        
        
    }

    public async Task<List<GetProductsDto>> ListAllProducts()
    {
        try
        {
         var products= await context.Products
            .Select(p => new GetProductsDto
            {
               Id = p.Id,
               ProductName = p.ProductName,
               PricePerPiece = p.PricePerPiece,
               Weight = p.Weight
                
            }).ToListAsync();

            return products;
            
        }
        catch (Exception ex)
        {
            
            throw new Exception(ex.Message);
        }
            
    }

    public async Task<bool> UpdateProductPrice(int id, PatchProductPriceDto product)
    {
        try
        {
            Product item = await context.Products.FindAsync(id);
            if (item is null) return false;
            
            item.PricePerPiece = product.PricePerPiece;
            return true;
        }
        catch (Exception ex)
        {
            
            throw new Exception(ex.Message);
        }
    }
}
