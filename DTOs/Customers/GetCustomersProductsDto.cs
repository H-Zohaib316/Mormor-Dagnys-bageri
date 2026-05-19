
namespace MormorBageri.DTOs.Customers;

public class GetCustomersProductsDto
{
    public int CustomerId { get; set; }
    public string StoreName { get; set; }

    public List<CustomerProductsDto> Products { get; set; } = [];
}

public class CustomerProductsDto
{
    public string ProductName { get; set; }
    public decimal PricePerPiece { get; set; }
    public int Quantity { get; set; }
    
}

