using System.Diagnostics.CodeAnalysis;

namespace MormorBageri.Entities;

public class Supplier
{
    public int SupplierId { get; set; }
    
    [NotNull]
    public string SupplierName { get; set; }
    [NotNull]
    public string Address { get; set; }
    public string ContactPerson { get; set; }
    [NotNull]
    public string Phone { get; set; }
    [NotNull]
    public string Email { get; set; }

    public List<SupplierProduct> SupplierProducts { get; set;}


}
