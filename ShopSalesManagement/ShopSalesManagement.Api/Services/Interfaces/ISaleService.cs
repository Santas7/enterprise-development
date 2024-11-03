using ShopSalesManagement.Api.DTOs;
using System.Collections.Generic;

namespace ShopSalesManagement.Api.Services;

public interface ISaleService
{
    IEnumerable<SaleDTO> GetAll();
    SaleDTO GetById(int id);
    SaleDTO Create(SaleDTO saleDto);
    bool Update(int id, SaleDTO saleDto);
    bool Delete(int id);
    IEnumerable<StoreDTO> GetStoresWithSalesAbove(decimal threshold, DateTime startDate, DateTime endDate);
}
