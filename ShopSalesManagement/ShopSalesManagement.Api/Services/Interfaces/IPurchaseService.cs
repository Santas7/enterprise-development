using ShopSalesManagement.Api.DTOs;
using System.Collections.Generic;

namespace ShopSalesManagement.Api.Services;

public interface IPurchaseService
{
    IEnumerable<PurchaseDTO> GetAll();
    PurchaseDTO? GetById(int id);
    PurchaseDTO Create(PurchaseDTO purchaseDto);
    bool Update(int id, PurchaseDTO purchaseDto);
    bool Delete(int id);
    IEnumerable<PurchaseDTO> GetTopPurchases(int topN);
}
