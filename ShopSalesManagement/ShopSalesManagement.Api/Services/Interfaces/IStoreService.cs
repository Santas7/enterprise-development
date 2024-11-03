using ShopSalesManagement.Api.DTOs;
using System.Collections.Generic;

namespace ShopSalesManagement.Api.Services;

public interface IStoreService
{
    IEnumerable<StoreDTO> GetAll();
    StoreDTO? GetById(int id);
    StoreDTO Create(StoreDTO storeDto);
    bool Update(int id, StoreDTO storeDto);
    bool Delete(int id);
    IEnumerable<StoreDTO> GetStoresWithStockForProduct(int productId);
    IEnumerable<ProductDTO> GetProductsByStoreId(int storeId); 
    Dictionary<int, Dictionary<int, decimal>> GetAveragePriceByProductGroup();
}
