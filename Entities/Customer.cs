namespace MormorBageri.Entities;

public class Customer : BaseEntity
{
 public string StoreName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string ContactPerson { get; set; }

    public string DeliveryAddress { get; set; }
    public string BillingAddress { get; set; }

    public List<SalesOrder> Orders { get; set; } = [];
}


