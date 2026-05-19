using MormorBageri.DTOs.SalesOrders;

namespace MormorBageri.DTOs.Customers;

public class GetCustomerDto : GetAllCustomersDto
{
   public string Phone { get; set; }
    public string DeliveryAddress { get; set; }

    public string BillingAddress { get; set; }

    public List<OrderDto> Orders { get; set; }
}
