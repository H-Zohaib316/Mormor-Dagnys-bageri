using MormorBageri.DTOs.Products;

namespace MormorBageri.interfaces;

public interface IProductRepository
{
    public Task<List<GetProductsDto>> ListAllProducts();
    public Task<GetProductDto> FindProduct(int id);
    public Task<bool> AddProduct(PostProductDto product);
    public Task<bool> UpdateProductPrice(int id, PatchProductPriceDto product);



}
