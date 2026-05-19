namespace MormorBageri.DTOs.SalesOrders;

public class GetOrderDto : BaseSalesOrder
{
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string DeliveryAddress { get; set; } 
    public List<OrderProductDto> Products { get; set; } = [];

}

public class OrderProductDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal TotalPrice { get; set; }


}
