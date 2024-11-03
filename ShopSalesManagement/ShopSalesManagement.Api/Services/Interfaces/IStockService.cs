using ShopSalesManagement.Api.DTOs;
using System.Collections.Generic;

namespace ShopSalesManagement.Api.Services
{
    public interface IStockService
    {
        IEnumerable<StockDTO> GetAll();
        StockDTO GetById(int id);
        StockDTO Create(StockDTO stockDto);
        bool Update(int id, StockDTO stockDto);
        bool Delete(int id);
        IEnumerable<StockDTO> GetStockByStore(int storeId);
        IEnumerable<StockDTO> GetStockBelowThreshold(int threshold);
        IEnumerable<ProductDTO> GetExpiredProducts();
    }
}
