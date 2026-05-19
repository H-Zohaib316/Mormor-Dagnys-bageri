using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MormorBageri.Data;
using MormorBageri.DTOs;
using MormorBageri.DTOs.Products;
using MormorBageri.Entities;
using MormorBageri.interfaces;

namespace MormorBageri.Controllers;

[Route("api/products")]
[ApiController]
public class ProductsController(IUnitOfWork unitOfWork) : ControllerBase
{
    [HttpGet()]
    public async Task<ActionResult> ListAllProducts()
    {
        var products = await unitOfWork.ProductRepository.ListAllProducts();
            
        return Ok(new
        {
            Success= true,
            StatusCode=200,
            Items=products.Count,
            Data = products

        });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> FindProduct(int id)
    {
        var product = await unitOfWork.ProductRepository.FindProduct(id);
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


    [HttpPost()]
    public async Task<ActionResult> AddProduct(PostProductDto model)
    {
        try
        {
        if (await unitOfWork.ProductRepository.AddProduct(model))
        {
            await unitOfWork.Complete();
            return StatusCode(201);

        }
            return StatusCode(500, "Ett server fel inträffade");
            
        }
        catch
        {
            
            return StatusCode(500, "Ett server fel inträffade");
        }
        
    } 

    [HttpPatch("{id}")]
    public async Task<ActionResult> UpdateProductPrice(int id, PatchProductPriceDto model)
    {
        try
        {
        if (await unitOfWork.ProductRepository.UpdateProductPrice(id, model))
        {
            await unitOfWork.Complete();
            return NoContent();
        }

        return StatusCode(500, "Något Serverfel inträffade");
            
        }
        catch 
        {
            
            return StatusCode(500, "Något Serverfel inträffade");

        }

        
    }
    
   
    
}

