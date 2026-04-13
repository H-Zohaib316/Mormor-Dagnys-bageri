using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MormorBageri.Data;
using MormorBageri.DTOs;
using MormorBageri.Entities;

namespace MormorBageri.Controllers;

[Route("api/products")]
[ApiController]
public class ProductsController(EShopContext context) : ControllerBase
{
    [HttpGet()]
    public async Task<ActionResult> ListAllProducts()
    {
        var products = await context.Products
            .Include(p => p.SupplierProducts)
            .ThenInclude(sp => sp.Supplier )
            .Select( p=> new
            {
                p.Id,
                p.ItemNumber,
                p.ProductName,
                Suppliers = p.SupplierProducts.Select(sp => new
                {
                    sp.Supplier.SupplierName,
                    sp.PricePerKg

                })
            }).ToListAsync();
        return Ok(new
        {
            Success= true,
            StatusCode=200,
            Items=products.Count,
            Data = products

        });
    }

    [HttpGet("{productName}")]
    public async Task<ActionResult> FindProduct(string productName)
    {
        var product = await context.Products
            .Where(p=> p.ProductName == productName)
            .Include(p=> p.SupplierProducts)
            .ThenInclude(sp => sp.Supplier)
            .Select(p=> new
            {
                p.Id,
                p.ItemNumber,
                p.ProductName,
                Suppliers = p.SupplierProducts.Select(sp => new
                {
                    sp.SupplierId,
                    sp.Supplier.SupplierName,
                    sp.PricePerKg
                })
                
            }).SingleOrDefaultAsync();
        if(product is not null)
        {
            return Ok(new
            {
                Success=true,
                StatusCode=200,
                Items=1,
                Data = product
            });
        }
           
        
        return NotFound();
    }
    
   
    
}

