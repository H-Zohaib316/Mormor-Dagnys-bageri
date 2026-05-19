using System.ComponentModel.DataAnnotations;

namespace MormorBageri.DTOs.Products;

public class PostProductDto 
{
    [Required(ErrorMessage = "Name is required")]
    public required string ProductName { get; set; }
    [Required(ErrorMessage = "Price is required")]
    public decimal PricePerPiece { get; set; }
    [Required(ErrorMessage = "Weight is required")]
    public double Weight { get; set; }
    [Required(ErrorMessage = "PackageAmount is required")]
    public int PackageAmount { get; set; }
    [Required(ErrorMessage = "BestBeforeDate is required")]
    public DateOnly BestBeforeDate { get; set; }
    [Required(ErrorMessage = "ManufacturedDate is required")]
    public DateOnly ManufacturedDate { get; set; }


}
