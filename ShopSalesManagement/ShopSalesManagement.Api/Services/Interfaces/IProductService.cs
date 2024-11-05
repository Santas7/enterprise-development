using ShopSalesManagement.Api.DTOs;

namespace ShopSalesManagement.Api.Services.Interfaces;

public interface IProductService
{
    IEnumerable<ProductDto> GetAll();
    ProductDto? GetById(int id);
    ProductDto Create(ProductCreateDto productCreateDto);
    bool Update(int id, ProductDto productDto);
    bool Delete(int id);
    IEnumerable<StoreDto> GetStoresWithProduct(int productId);
}
