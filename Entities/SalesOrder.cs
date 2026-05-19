namespace MormorBageri.Entities;

public class SalesOrder : BaseEntity
{
public DateTime OrderDate { get; set; }

    public string OrderNumber { get; set; } = string.Empty;

    public int CustomerId { get; set; }

    public Customer Customer { get; set; }

    public List<OrderItem> OrderItems { get; set; } = [];
    

}
