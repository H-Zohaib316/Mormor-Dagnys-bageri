using System.Diagnostics.CodeAnalysis;

namespace MormorBageri.Entities;

public class Product
{
    public int Id { get; set; }
    [NotNull]
    public string ItemNumber { get; set; }
    [NotNull]
    public string ProductName { get; set; }
    [NotNull]
    public decimal Price { get; set; }
    public List<SupplierProduct> SupplierProducts { get; set; }


}
