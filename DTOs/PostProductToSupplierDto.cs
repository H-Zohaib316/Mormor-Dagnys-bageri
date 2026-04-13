using System.ComponentModel.DataAnnotations;

namespace MormorBageri.DTOs;

public record PostProductToSupplierDto
{
    [Required]
    public string ItemNumber { get; set; }
    [Required]
    public string ProductName { get; set; }
    public decimal PricePerKg { get; set; }

}
