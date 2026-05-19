namespace MormorBageri.Entities;

public class OrderItem : BaseEntity
{
    public int OrderId { get; set; }
    public SalesOrder SalesOrder { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; }

    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal TotalPrice { get; set; }
}