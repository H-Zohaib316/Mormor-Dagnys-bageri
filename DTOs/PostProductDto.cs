namespace MormorBageri.DTOs;

public record PostProductDto
{
    public int SupplierId { get; set; }
    public string ItemNumber { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    
    


}
