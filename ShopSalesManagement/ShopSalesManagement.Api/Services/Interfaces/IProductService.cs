using ShopSalesManagement.Api.DTOs;

namespace ShopSalesManagement.Api.Services;

public interface IProductService
{
    IEnumerable<ProductDto> GetAll();
    ProductDto? GetById(int id);
    ProductDto Create(ProductDto productDto);
    bool Update(int id, ProductDto productDto);
    bool Delete(int id);
    IEnumerable<StoreDto> GetStoresWithProduct(int productId);
}
