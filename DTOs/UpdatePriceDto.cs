namespace MormorBageri.DTOs;

public record UpdatePriceDto
{
    public int SupplierId { get; set; }
    public int ProductId { get; set; }
    public decimal PricePerKg { get; set; }


}
