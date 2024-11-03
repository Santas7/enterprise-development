using ShopSalesManagement.Api.DTOs;

namespace ShopSalesManagement.Api.Services
{
    public interface IProductService
    {
        IEnumerable<ProductDTO> GetAll();
        ProductDTO? GetById(int id);
        ProductDTO Create(ProductDTO productDto);
        bool Update(int id, ProductDTO productDto);
        bool Delete(int id);
        IEnumerable<StoreDTO> GetStoresWithProduct(int productId);
    }
}
