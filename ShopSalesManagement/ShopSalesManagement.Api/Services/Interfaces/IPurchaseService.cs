using ShopSalesManagement.Api.DTOs;

namespace ShopSalesManagement.Api.Services.Interfaces;

public interface IPurchaseService
{
    IEnumerable<PurchaseDto> GetAll();
    PurchaseDto? GetById(int id);
    PurchaseDto Create(PurchaseCreateDto purchaseCreateDto);
    bool Update(int id, PurchaseDto purchaseDto);
    bool Delete(int id);
    IEnumerable<PurchaseDto> GetTopPurchases(int topN);
}
