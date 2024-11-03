using ShopSalesManagement.Api.DTOs;
using ShopSalesManagement.Domain;

using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopSalesManagement.Api.Services;

public class SaleService : ISaleService
{
    private readonly ApplicationDbContext _context;

    public SaleService(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<SaleDTO> GetAll()
    {
        return _context.Sales.Select(s => new SaleDTO
        {
            Id = s.Id,
            SaleDate = s.SaleDate,
            CustomerId = s.CustomerId,
            StoreId = s.StoreId,
            TotalAmount = s.TotalAmount
        }).ToList();
    }

    public SaleDTO? GetById(int id)
    {
        var sale = _context.Sales.Find(id);
        return sale == null ? null : new SaleDTO
        {
            Id = sale.Id,
            SaleDate = sale.SaleDate,
            CustomerId = sale.CustomerId,
            StoreId = sale.StoreId,
            TotalAmount = sale.TotalAmount
        };
    }

    public SaleDTO Create(SaleDTO saleDto)
    {
        var sale = new Sale(saleDto.SaleDate, saleDto.CustomerId, saleDto.StoreId, saleDto.TotalAmount);
        _context.Sales.Add(sale);
        _context.SaveChanges();
        saleDto.Id = sale.Id;
        return saleDto;
    }


    public bool Update(int id, SaleDTO saleDto)
    {
        var sale = _context.Sales.Find(id);
        if (sale == null) return false;

        sale.SaleDate = saleDto.SaleDate;
        sale.CustomerId = saleDto.CustomerId;
        sale.StoreId = saleDto.StoreId;
        sale.TotalAmount = saleDto.TotalAmount;
        _context.SaveChanges();
        return true;
    }

    public bool Delete(int id)
    {
        var sale = _context.Sales.Find(id);
        if (sale == null) return false;

        _context.Sales.Remove(sale);
        _context.SaveChanges();
        return true;
    }

    public IEnumerable<StoreDTO> GetStoresWithSalesAbove(decimal threshold, DateTime startDate, DateTime endDate)
    {
        return _context.Sales
            .Where(s => s.TotalAmount > threshold && s.SaleDate >= startDate && s.SaleDate <= endDate)
            .Select(s => new StoreDTO
            {
                Id = s.Store!.Id,
                Name = s.Store.Name ?? string.Empty,
                Address = s.Store.Address ?? string.Empty
            }).Distinct().ToList();
    }
}
