
namespace MormorBageri.Entities;

public class Product : BaseEntity
{
    public required string ProductName { get; set; }
    public decimal PricePerPiece { get; set; }
    public double Weight { get; set; }
    public int PackageAmount { get; set; }
    public DateOnly BestBeforeDate { get; set; }
    public DateOnly ManufacturedDate { get; set; }
    public int SupplierId { get; set; }

    public List<OrderItem> OrderItems { get; set; } = [];
    public List<SupplierProduct> SupplierProducts { get; set; } = [];


}
