using ShopSalesManagement.Api.DTOs;
using System.Collections.Generic;

namespace ShopSalesManagement.Api.Services;

public interface IPurchaseService
{
    IEnumerable<PurchaseDto> GetAll();
    PurchaseDto? GetById(int id);
    PurchaseDto Create(PurchaseDto purchaseDto);
    bool Update(int id, PurchaseDto purchaseDto);
    bool Delete(int id);
    IEnumerable<PurchaseDto> GetTopPurchases(int topN);
}
