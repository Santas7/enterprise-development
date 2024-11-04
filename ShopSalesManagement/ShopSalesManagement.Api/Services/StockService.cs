using ShopSalesManagement.Api.DTOs;
using ShopSalesManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopSalesManagement.Api.Services;

public class StockService : IStockService
{
    private readonly ApplicationDbContext _context;

    public StockService(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<StockDto> GetAll()
    {
        return _context.Stocks.Select(s => new StockDto
        {
            Id = s.Id,
            ProductId = s.ProductId,
            StoreId = s.StoreId,
            Quantity = s.Quantity
        }).ToList();
    }

    public StockDto? GetById(int id)
    {
        var stock = _context.Stocks.Find(id);
        return stock == null ? null : new StockDto
        {
            Id = stock.Id,
            ProductId = stock.ProductId,
            StoreId = stock.StoreId,
            Quantity = stock.Quantity
        };
    }

    public StockDto Create(StockDto stockDto)
    {
        var stock = new Stock
        {
            ProductId = stockDto.ProductId,
            StoreId = stockDto.StoreId,
            Quantity = stockDto.Quantity
        };
        _context.Stocks.Add(stock);
        _context.SaveChanges();
        stockDto.Id = stock.Id;
        return stockDto;
    }

    public bool Update(int id, StockDto stockDto)
    {
        var stock = _context.Stocks.Find(id);
        if (stock == null) return false;

        stock.ProductId = stockDto.ProductId;
        stock.StoreId = stockDto.StoreId;
        stock.Quantity = stockDto.Quantity;
        _context.SaveChanges();
        return true;
    }

    public bool Delete(int id)
    {
        var stock = _context.Stocks.Find(id);
        if (stock == null) return false;

        _context.Stocks.Remove(stock);
        _context.SaveChanges();
        return true;
    }

    public IEnumerable<StockDto> GetStockByStore(int storeId)
    {
        return _context.Stocks
            .Where(s => s.StoreId == storeId)
            .Select(s => new StockDto
            {
                Id = s.Id,
                ProductId = s.ProductId,
                StoreId = s.StoreId,
                Quantity = s.Quantity
            }).ToList();
    }

    public IEnumerable<StockDto> GetStockBelowThreshold(int threshold)
    {
        return _context.Stocks
            .Where(s => s.Quantity < threshold)
            .Select(s => new StockDto
            {
                Id = s.Id,
                ProductId = s.ProductId,
                StoreId = s.StoreId,
                Quantity = s.Quantity
            }).ToList();
    }

    public IEnumerable<ProductDto> GetExpiredProducts()
    {
        var today = DateTime.Today; 
        return _context.Stocks
            .Where(stock => stock.Quantity > 0 && stock.Product!.ExpirationDate < today) 
            .Select(stock => new ProductDto
            {
                Id = stock.Product!.Id,
                Barcode = stock.Product.Barcode,
                ProductGroupId = stock.Product.ProductGroupId,
                Name = stock.Product.Name,
                PackageWeight = stock.Product.Weight,
                Type = stock.Product.Type,
                Price = stock.Product.Price,
                ExpirationDate = stock.Product.ExpirationDate
            }).ToList();
    }
}
