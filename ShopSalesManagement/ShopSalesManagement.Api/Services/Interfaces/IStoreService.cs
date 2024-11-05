using ShopSalesManagement.Api.DTOs;

namespace ShopSalesManagement.Api.Services.Interfaces;

public interface IStoreService
{
    IEnumerable<StoreDto> GetAll();
    StoreDto? GetById(int id);
    StoreDto Create(StoreDto storeDto);
    bool Update(int id, StoreDto storeDto);
    bool Delete(int id);
    IEnumerable<StoreDto> GetStoresWithStockForProduct(int productId);
    IEnumerable<ProductDto> GetProductsByStoreId(int storeId); 
    Dictionary<int, Dictionary<int, decimal>> GetAveragePriceByProductGroup();
}
