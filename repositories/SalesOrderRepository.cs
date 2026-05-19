using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using MormorBageri.Data;
using MormorBageri.DTOs.OrderItems;
using MormorBageri.DTOs.SalesOrders;
using MormorBageri.Entities;
using MormorBageri.interfaces;

namespace MormorBageri.repositories;

public class SalesOrderRepository(EShopContext context) : ISalesOrderRepository
{
    public async Task<bool> AddOrder(PostSalesOrderDto order)
    {
        try
        {
        Customer customer = await context.Customers
        .FirstOrDefaultAsync(c => c.Id == order.CustomerId)
        ?? throw new Exception("Hittade ingen kund");

        SalesOrder item = new()
    {
        CustomerId = order.CustomerId,
        OrderNumber = order.OrderNumber,
        OrderDate = order.OrderDate,

        OrderItems = [.. order.Items.Select(oi=> new OrderItem
        {
            ProductId = oi.ProductId,
            Quantity = oi.Quantity

            
        })]
        };

    context.SalesOrders.Add(item);


    return true;
            
        }
        catch (Exception ex)
        {
            
            throw new Exception(ex.Message);
        }
        
    }

    public async Task<bool> DeleteOrder(int id)
    {
         SalesOrder order = await context.SalesOrders.FindAsync(id);
            if (order is null) return false;

            context.SalesOrders.Remove(order);
            return true;
    }

    public async Task<List<CustomerOrderDto>>FindOrderByDate(DateTime date)
    {
       try
       {
         var order = await context.SalesOrders
            .Include(o=> o.Customer)
            .Where(o=> o.OrderDate.Date == date.Date)
            .Select(o => new CustomerOrderDto
            {
                Id = o.Id,
                OrderNumber = o.OrderNumber,
                OrderDate = o.OrderDate,
                CustomerId = o.CustomerId,
                CustomerName = o.Customer.StoreName
            }).ToListAsync();
                
                return order;

        
            

       }
       catch (Exception ex)
       {
        
            throw new Exception(ex.Message);

       }
    }

    public async Task<GetOrderDto> FindOrderById(int id)
    {
        try
        {
            var order = await context.SalesOrders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                .ThenInclude(o=> o.Product)
                .Where(o => o.Id == id)
                .Select(o => new GetOrderDto
                {
                    Id = o.Id,
                    OrderNumber = o.OrderNumber,
                    OrderDate = o.OrderDate,
                    CustomerId = o.CustomerId,
                    CustomerName = o.Customer.StoreName,
                    CustomerEmail = o.Customer.Email,
                    DeliveryAddress = o.Customer.DeliveryAddress,

                    Products = o.OrderItems.Select(oi=> new OrderProductDto
                    {
                        ProductId = oi.ProductId,
                        ProductName = oi.Product.ProductName,
                        Quantity = oi.Quantity,
                        Price = oi.Product.PricePerPiece,
                        TotalPrice = oi.Quantity * oi.Product.PricePerPiece
                    } ).ToList()  
                }).FirstOrDefaultAsync();
                
                return order;
            
        }
        catch (Exception ex)
        {
            
            throw new Exception(ex.Message);
        }
    }

    public async Task<GetOrderDto> FindOrderByOrderNumber(string orderNumber)
    {
        try
        {
            var order = await context.SalesOrders
                .Include(o=> o.Customer)
                .Include(o=> o.OrderItems)
                .ThenInclude(oi=> oi.Product)
                .Where(o=> o.OrderNumber == orderNumber)
                .Select(o => new GetOrderDto
                {
                    Id = o.Id,
                    OrderNumber = o.OrderNumber,
                    OrderDate = o.OrderDate,
                    CustomerId = o.CustomerId,
                    CustomerName = o.Customer.StoreName,
                    CustomerEmail = o.Customer.Email,
                    DeliveryAddress = o.Customer.DeliveryAddress,

                    Products = o.OrderItems.Select(oi=> new OrderProductDto
                    {
                        ProductId = oi.ProductId,
                        ProductName = oi.Product.ProductName,
                        Quantity = oi.Quantity,
                        Price = oi.Product.PricePerPiece,
                        TotalPrice = oi.Quantity * oi.Product.PricePerPiece
                    }).ToList()
                    
                }).FirstOrDefaultAsync();
                
                return order;
                
        }
        catch (Exception ex)
        {
            
            throw new Exception(ex.Message);
        }
    }

    public async Task<List<GetOrdersDto>> ListAllOrders()
    {
        try
        {
            var orders= await context.SalesOrders
                .Include(o=> o.Customer)
                .Select(o=> new GetOrdersDto
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate,
                    OrderNumber = o.OrderNumber,
                    CustomerId = o.CustomerId,
                    CustomerName = o.Customer.StoreName  
                }).ToListAsync();
            
            return orders;
            
        }
        catch (Exception ex)
        {
            
            throw new Exception(ex.Message);
        }
    }

}
