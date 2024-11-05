using ShopSalesManagement.Api.DTOs;
using ShopSalesManagement.Api.Services.Interfaces;
using ShopSalesManagement.Domain;

namespace ShopSalesManagement.Api.Services;

public class SaleService : ISaleService
{
    private readonly ApplicationDbContext _context;

    public SaleService(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<SaleDto> GetAll()
    {
        return _context.Sales.Select(s => new SaleDto
        {
            Id = s.Id,
            SaleDate = s.SaleDate,
            CustomerId = s.CustomerId,
            StoreId = s.StoreId,
            TotalAmount = s.TotalAmount
        }).ToList();
    }

    public SaleDto? GetById(int id)
    {
        var sale = _context.Sales.Find(id);
        return sale == null ? null : new SaleDto
        {
            Id = sale.Id,
            SaleDate = sale.SaleDate,
            CustomerId = sale.CustomerId,
            StoreId = sale.StoreId,
            TotalAmount = sale.TotalAmount
        };
    }

    public SaleDto Create(SaleCreateDto saleCreateDto)
    {
        var sale = new Sale(saleCreateDto.SaleDate, saleCreateDto.CustomerId, saleCreateDto.StoreId, saleCreateDto.TotalAmount);
        _context.Sales.Add(sale);
        _context.SaveChanges();

        return new SaleDto
        {
            Id = sale.Id,
            SaleDate = sale.SaleDate,
            CustomerId = sale.CustomerId,
            StoreId = sale.StoreId,
            TotalAmount = sale.TotalAmount
        };
    }


    public bool Update(int id, SaleDto saleDto)
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

    public IEnumerable<StoreDto> GetStoresWithSalesAbove(decimal threshold, DateTime startDate, DateTime endDate)
    {
        return _context.Sales
            .Where(s => s.TotalAmount > threshold && s.SaleDate >= startDate && s.SaleDate <= endDate)
            .Select(s => new StoreDto
            {
                Id = s.Store!.Id,
                Name = s.Store.Name ?? string.Empty,
                Address = s.Store.Address ?? string.Empty
            }).Distinct().ToList();
    }
}
