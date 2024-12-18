﻿using ShopSalesManagement.Api.DTOs;

namespace ShopSalesManagement.Api.Services.Interfaces;

public interface IStockService
{
    IEnumerable<StockDto> GetAll();
    StockDto? GetById(int id);
    StockDto Create(StockCreateDto stockCreateDto);
    bool Update(int id, StockDto stockDto);
    bool Delete(int id);
    IEnumerable<StockDto> GetStockByStore(int storeId);
    IEnumerable<StockDto> GetStockBelowThreshold(int threshold);
    IEnumerable<ProductDto> GetExpiredProducts();
}
