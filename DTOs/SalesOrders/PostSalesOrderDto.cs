using System.Security.AccessControl;
using MormorBageri.DTOs.OrderItems;

namespace MormorBageri.DTOs.SalesOrders;

public class PostSalesOrderDto 
{

    public int CustomerId { get; set; }
    public string OrderNumber { get; set; }
    public DateTime OrderDate { get; set; }

    public List<PostOrderItemDto> Items { get; set;}

}
