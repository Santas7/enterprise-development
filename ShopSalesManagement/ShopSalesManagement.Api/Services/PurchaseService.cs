using ShopSalesManagement.Api.DTOs;
using ShopSalesManagement.Domain;
using System.Collections.Generic;
using System.Linq;

namespace ShopSalesManagement.Api.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly ApplicationDbContext _context;

        public PurchaseService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<PurchaseDTO> GetAll()
        {
            return _context.Purchases.Select(p => new PurchaseDTO
            {
                Id = p.Id,
                SaleId = p.SaleId,
                ProductId = p.ProductId,
                Quantity = p.Quantity,
                UnitPrice = p.UnitPrice,
                TotalPrice = p.TotalPrice
            }).ToList();
        }

        public PurchaseDTO GetById(int id)
        {
            var purchase = _context.Purchases.Find(id);
            return purchase == null ? null : new PurchaseDTO
            {
                Id = purchase.Id,
                SaleId = purchase.SaleId,
                ProductId = purchase.ProductId,
                Quantity = purchase.Quantity,
                UnitPrice = purchase.UnitPrice,
                TotalPrice = purchase.TotalPrice
            };
        }

        public PurchaseDTO Create(PurchaseDTO purchaseDto)
        {
            var purchase = new Purchase
            {
                SaleId = purchaseDto.SaleId,
                ProductId = purchaseDto.ProductId,
                Quantity = purchaseDto.Quantity,
                UnitPrice = purchaseDto.UnitPrice,
                TotalPrice = purchaseDto.TotalPrice
            };
            _context.Purchases.Add(purchase);
            _context.SaveChanges();
            purchaseDto.Id = purchase.Id;
            return purchaseDto;
        }

        public bool Update(int id, PurchaseDTO purchaseDto)
        {
            var purchase = _context.Purchases.Find(id);
            if (purchase == null) return false;

            purchase.Quantity = purchaseDto.Quantity;
            purchase.UnitPrice = purchaseDto.UnitPrice;
            purchase.TotalPrice = purchaseDto.TotalPrice;
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var purchase = _context.Purchases.Find(id);
            if (purchase == null) return false;

            _context.Purchases.Remove(purchase);
            _context.SaveChanges();
            return true;
        }

        public IEnumerable<PurchaseDTO> GetTopPurchases(int topN)
        {
            return _context.Purchases
                .OrderByDescending(p => p.TotalPrice)
                .Take(topN)
                .Select(p => new PurchaseDTO
                {
                    Id = p.Id,
                    SaleId = p.SaleId,
                    ProductId = p.ProductId,
                    Quantity = p.Quantity,
                    UnitPrice = p.UnitPrice,
                    TotalPrice = p.TotalPrice
                }).ToList();
        }
    }
}
