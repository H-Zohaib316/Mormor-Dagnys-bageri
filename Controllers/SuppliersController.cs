using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MormorBageri.Data;
using MormorBageri.DTOs;
using MormorBageri.Entities;

namespace MormorBageri.Controllers;

[Route("api/suppliers")]
[ApiController]
public class SuppliersController(EShopContext context) : ControllerBase
{
    [HttpGet()]
    public async Task<ActionResult> ListAllSuppliers()
    {
        var suppliers = await context.Suppliers
            .Include(s=> s.SupplierProducts)
            .ThenInclude(sp => sp.Product)
            .Select(s => new
            {
                s.SupplierId,
                s.SupplierName,
                Products= s.SupplierProducts.Select(sp => new
                {
                    sp.ProductId,
                    sp.Product.ItemNumber,
                    sp.Product.ProductName,
                    sp.PricePerKg
                })
            
            }).ToListAsync();
    
            

            
        return Ok(new{Success=true, StatusCode=200, Items=suppliers.Count, Data=suppliers});
        
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> FindSupplier(int id)
    {
        Supplier supplier = await context.Suppliers
            .Include(s=> s.SupplierProducts)
            .ThenInclude(sp=> sp.Product)
            .SingleOrDefaultAsync(s=> s.SupplierId == id);

        var SupplierToReturn = new
        {
            supplier.SupplierId,
            supplier.SupplierName,
            supplier.Address,
            supplier.ContactPerson,
            Products = supplier.SupplierProducts.Select(sp => new
            {
                sp.Product.ItemNumber,
                sp.Product.ProductName,
                sp.PricePerKg
 
            })
        };

        return Ok(new{Success= true, StatusCode=200, Items=1, Data=SupplierToReturn});
    }

    [HttpPost("{supplierId}/products")]
    public async Task<ActionResult> AddProductToSupplier(int supplierId, PostProductToSupplierDto model)
    {
        var supplier = await context.Suppliers
            .FirstOrDefaultAsync(s=> s.SupplierId == supplierId);

        if(supplier is null) return NotFound("Hittade ingen leverantör");

        var product = await context.Products
            .FirstOrDefaultAsync(p=> p.ItemNumber == model.ItemNumber);
        if (product is null)
        {
                product = new()
                {
                    ItemNumber = model.ItemNumber,
                    ProductName = model.ProductName,
                    Price = model.PricePerKg
                    
                };
            context.Products.Add(product);
                
        }
        await context.SaveChangesAsync();
        var exists = await context.SupplierProducts
            .AnyAsync(sp=> sp.SupplierId == supplier.SupplierId 
                && sp.ProductId == product.Id);

        if(exists) return BadRequest("Produkt finns redan hos vald leverantör");

        SupplierProduct supplierProduct = new()
        {
            SupplierId = supplier.SupplierId,
            ProductId = product.Id,
            PricePerKg = model.PricePerKg
            
        };
      
        context.SupplierProducts.Add(supplierProduct);
        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(FindSupplier), new{id = product.Id}, model);

        
    }


    [HttpPost()]
    public async Task<ActionResult> AddSupplier(PostSupplierDto model)
    {
        var exists = await context.Suppliers
            .AnyAsync(s=> s.SupplierName == model.SupplierName);

        if (exists)
        {
            return BadRequest("Leverantören finns redan");
        }

        var supplier = new Supplier
        {
            SupplierName = model.SupplierName,
            Address = model.Address,
            ContactPerson = model.ContactPerson,
            Phone = model.Phone,
            Email = model.Email
        };
        context.Suppliers.Add(supplier);
        await context.SaveChangesAsync();
        var result = new
        {
        supplier.SupplierId,
        supplier.SupplierName,
        supplier.ContactPerson,
        supplier.Phone,
        supplier.Email
            
        };
        return CreatedAtAction(nameof(FindSupplier), new{id = supplier.SupplierId}, result);


    }

    



    [HttpPut("price")]
    public async Task<ActionResult> UpdatePrice(UpdatePriceDto model)
    {
        if (model.PricePerKg <= 0)
        {
            return BadRequest("Pris måste vara större än 0");  
        }

        var supplierProduct = await context.SupplierProducts
            .FirstOrDefaultAsync(sp=> 
                sp.SupplierId == model.SupplierId &&
                sp.ProductId == model.ProductId
                
            );

        if(supplierProduct == null) return NotFound("Produkt finns ej");

        supplierProduct.PricePerKg = model.PricePerKg;
        await context.SaveChangesAsync();
        return Ok(new
        {
        message= "Price updated",
        model.SupplierId,
        model.ProductId,
        model.PricePerKg
        });
    }
    
}


