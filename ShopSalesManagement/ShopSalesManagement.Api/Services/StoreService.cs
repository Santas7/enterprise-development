using ShopSalesManagement.Api.DTOs;
using ShopSalesManagement.Domain;
using System.Collections.Generic;
using System.Linq;

namespace ShopSalesManagement.Api.Services
{
    public class StoreService : IStoreService
    {
        private readonly ApplicationDbContext _context;

        public StoreService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<StoreDTO> GetAll()
        {
            return _context.Stores.Select(s => new StoreDTO
            {
                Id = s.Id,
                Name = s.Name,
                Address = s.Address
            }).ToList();
        }

        public StoreDTO GetById(int id)
        {
            var store = _context.Stores.Find(id);
            return store == null ? null : new StoreDTO
            {
                Id = store.Id,
                Name = store.Name,
                Address = store.Address
            };
        }

        public StoreDTO Create(StoreDTO storeDto)
        {
            var store = new Store
            {
                Name = storeDto.Name,
                Address = storeDto.Address
            };
            _context.Stores.Add(store);
            _context.SaveChanges();
            storeDto.Id = store.Id; // ID вновь созданного магазина
            return storeDto;
        }

        public bool Update(int id, StoreDTO storeDto)
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

        public IEnumerable<ProductDTO> GetProductsByStoreId(int storeId)
        {
            return _context.Stocks
                .Where(stock => stock.StoreId == storeId && stock.Quantity > 0)
                .Select(stock => new ProductDTO
                {
                    Id = stock.Product.Id,
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

        public IEnumerable<StoreDTO> GetStoresWithStockForProduct(int productId)
        {
            return _context.Stocks
                .Where(stock => stock.ProductId == productId && stock.Quantity > 0)
                .Select(stock => new StoreDTO
                {
                    Id = stock.Store.Id,
                    Name = stock.Store.Name,
                    Address = stock.Store.Address
                }).Distinct().ToList();
        }
    }
}
