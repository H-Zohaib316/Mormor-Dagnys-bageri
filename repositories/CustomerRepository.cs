using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MormorBageri.Data;
using MormorBageri.DTOs.Customers;
using MormorBageri.DTOs.SalesOrders;
using MormorBageri.Entities;
using MormorBageri.interfaces;

namespace MormorBageri.repositories;

public class CustomerRepository(EShopContext context) : ICustomerRepository
{
    
    public async Task<bool> AddCustomer(PostCustomerDto customer)
    {
        try
        {
          Customer c = new()
          {
              StoreName = customer.StoreName,
              Phone = customer.Phone,
              Email = customer.Email,
              ContactPerson = customer.ContactPerson,
              DeliveryAddress = customer.DeliveryAddress,
              BillingAddress = customer.BillingAddress
        
          };

          await context.Customers.AddAsync(c);
          return true;
        }
        catch (Exception ex)
        {
            
            throw new Exception(ex.Message);
        }
    }

    
    public async Task<GetCustomerDto> FindCustomer(int id)
    {
        try
        {
            var customer =  await context.Customers
            .Include(c => c.Orders)
            .Where(c => c.Id == id)
            .Select(c => new GetCustomerDto
            {
                CustomerId = c.Id,
                StoreName = c.StoreName,
                Phone = c.Phone,
                Email = c.Email,
                ContactPerson = c.ContactPerson,
                DeliveryAddress = c.DeliveryAddress,
                BillingAddress = c.BillingAddress,

                Orders = c.Orders.Select(o => new OrderDto
                {
                    Id = o.Id,
                    OrderNumber = o.OrderNumber,
                    OrderDate = o.OrderDate                  
                    
                }).ToList()
            })
            .FirstOrDefaultAsync();    

            if (customer is null) return null;

            return customer;      
            
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
            
        } 
    }

    public async Task<List<GetAllCustomersDto>> ListAllCustomers()
    {
        try
        {
            var list = await context.Customers.ToListAsync();
            

            List<GetAllCustomersDto> customers = [.. list.Select
                (c => new GetAllCustomersDto()
                {
                    CustomerId = c.Id,
                    StoreName = c.StoreName,
                    ContactPerson = c.ContactPerson,
                    Email = c.Email

                    
                })];

            return customers;
        }
        catch (Exception ex)
        {
            
            throw new Exception(ex.Message);
        }
    }

    public async Task<List<GetCustomersProductsDto>> ListCustomerProducts()
    {
        try
        {
            var customerProduct = await context.Customers
                .Include(c=> c.Orders)
                .ThenInclude(c=> c.OrderItems)
                .ThenInclude(oi=> oi.Product)
                .Select(c=> new GetCustomersProductsDto
                {
                    CustomerId = c.Id,
                    StoreName = c.StoreName,

                    Products = c.Orders
                    .SelectMany(o=> o.OrderItems)
                     .Select(oi => new CustomerProductsDto
                {
                
                    ProductName = oi.Product.ProductName,
                    PricePerPiece = oi.Product.PricePerPiece,
                    Quantity = oi.Quantity
                }).ToList()
        }).ToListAsync();

        return customerProduct;
                
            
        }
        catch (Exception ex)
        {
            
           throw new Exception(ex.Message);
        }
    }

    public async Task<bool> UpdateCustomerContactPerson(int id, PatchCustomerContactDto contactPerson)
    {
        try
        {
            var customer = await context.Customers.FirstOrDefaultAsync(c=> c.Id == id);

            if (customer == null) return false;

            customer.ContactPerson = contactPerson.ContactPerson;

            return true;
            
            
        }
        catch (Exception ex)
        {
            
            throw new Exception(ex.Message);
        }
    }
}
