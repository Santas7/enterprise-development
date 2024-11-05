using ShopSalesManagement.Api.DTOs;
using ShopSalesManagement.Api.Services.Interfaces;
using ShopSalesManagement.Domain;

namespace ShopSalesManagement.Api.Services;

public class StoreService : IStoreService
{
    private readonly ApplicationDbContext _context;

    public StoreService(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<StoreDto> GetAll()
    {
        return _context.Stores.Select(s => new StoreDto
        {
            Id = s!.Id,
            Name = s.Name ?? string.Empty,
            Address = s.Address ?? string.Empty
        }).ToList();
    }

    public StoreDto? GetById(int id)
    {
        var store = _context.Stores.Find(id);
        return store == null ? null : new StoreDto
        {
            Id = store.Id,
            Name = store.Name ?? string.Empty,
            Address = store.Address ?? string.Empty
        };
    }

    public StoreDto Create(StoreCreateDto storeCreateDto)
    {
        var store = new Store
        {
            Name = storeCreateDto.Name,
            Address = storeCreateDto.Address
        };
        _context.Stores.Add(store);
        _context.SaveChanges();
        return new StoreDto
        {
            Id = store.Id,
            Name = store.Name,
            Address = store.Address
        };
    }

    public bool Update(int id, StoreDto storeDto)
    {
        var store = _context.Stores.Find(id);
        if (store == null) return false;

        store.Name = storeDto.Name;
        store.Address = storeDto.Address;
        _context.SaveChanges();
        return true;
    }

    public bool Delete(int id)
    {
        var store = _context.Stores.Find(id);
        if (store == null) return false;

        _context.Stores.Remove(store);
        _context.SaveChanges();
        return true;
    }

    public IEnumerable<ProductDto> GetProductsByStoreId(int storeId)
    {
        return _context.Stocks
            .Where(stock => stock.StoreId == storeId && stock.Quantity > 0)
            .Select(stock => new ProductDto
            {
                Id = stock.Product!.Id ,
                Barcode = stock.Product.Barcode,
                ProductGroupId = stock.Product.ProductGroupId,
                Name = stock.Product.Name,
                PackageWeight = stock.Product.Weight, 
                Type = stock.Product.Type,
                Price = stock.Product.Price,
                ExpirationDate = stock.Product.ExpirationDate
            }).ToList();
    }



    public Dictionary<int, Dictionary<int, decimal>> GetAveragePriceByProductGroup()
    {
        return _context.Products
            .Where(p => p.ProductGroupId > 0) 
            .GroupBy(p => p.ProductGroupId) 
            .ToDictionary(g => g.Key, g => new Dictionary<int, decimal>
            {
                { g.Key, g.Average(p => p.Price) }
            });
    }

    public IEnumerable<StoreDto> GetStoresWithStockForProduct(int productId)
    {
        return _context.Stocks
            .Where(stock => stock.ProductId == productId && stock.Quantity > 0 && stock.Store != null)
            .Select(stock => new StoreDto
            {
                Id = stock.Store!.Id,          
                Name = stock.Store.Name ?? string.Empty,
                Address = stock.Store.Address ?? string.Empty
            })
            .Distinct()
            .ToList();
    }

}
