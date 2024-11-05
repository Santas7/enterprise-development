using ShopSalesManagement.Api.DTOs;

namespace ShopSalesManagement.Api.Services.Interfaces;

public interface ISaleService
{
    IEnumerable<SaleDto> GetAll();
    SaleDto? GetById(int id);
    SaleDto Create(SaleCreateDto saleCreateDto);
    bool Update(int id, SaleDto saleDto);
    bool Delete(int id);
    IEnumerable<StoreDto> GetStoresWithSalesAbove(decimal threshold, DateTime startDate, DateTime endDate);
}
