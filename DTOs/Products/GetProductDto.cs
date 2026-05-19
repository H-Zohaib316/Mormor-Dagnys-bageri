namespace MormorBageri.DTOs.Products;

public class GetProductDto : GetProductsDto
{

    public int PackageAmount { get; set; }
    public DateOnly BestBeforeDate { get; set; }

    
    
    
    

}
