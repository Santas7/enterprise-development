﻿using ShopSalesManagement.Api.DTOs;
using ShopSalesManagement.Api.Services.Interfaces;
using ShopSalesManagement.Domain;

namespace ShopSalesManagement.Api.Services;

public class PurchaseService : IPurchaseService
{
    private readonly ApplicationDbContext _context;

    public PurchaseService(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<PurchaseDto> GetAll()
    {
        return _context.Purchases.Select(p => new PurchaseDto
        {
            Id = p.Id,
            SaleId = p.SaleId,
            ProductId = p.ProductId,
            Quantity = p.Quantity,
            UnitPrice = p.UnitPrice,
            TotalPrice = p.TotalPrice
        }).ToList();
    }

    public PurchaseDto? GetById(int id)
    {
        var purchase = _context.Purchases.Find(id);
        return purchase == null ? null : new PurchaseDto
        {
            Id = purchase.Id,
            SaleId = purchase.SaleId,
            ProductId = purchase.ProductId,
            Quantity = purchase.Quantity,
            UnitPrice = purchase.UnitPrice,
            TotalPrice = purchase.TotalPrice
        };
    }

    public PurchaseDto Create(PurchaseCreateDto purchaseCreateDto)
    {
        var purchase = new Purchase
        {
            SaleId = purchaseCreateDto.SaleId,
            ProductId = purchaseCreateDto.ProductId,
            Quantity = purchaseCreateDto.Quantity,
            UnitPrice = purchaseCreateDto.UnitPrice,
            TotalPrice = purchaseCreateDto.TotalPrice
        };
        _context.Purchases.Add(purchase);
        _context.SaveChanges();

        return new PurchaseDto
        {
            Id = purchase.Id,
            SaleId = purchase.SaleId,
            ProductId = purchase.ProductId,
            Quantity = purchase.Quantity,
            UnitPrice = purchase.UnitPrice,
            TotalPrice = purchase.TotalPrice
        };
    }

    public bool Update(int id, PurchaseDto purchaseDto)
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

    public IEnumerable<PurchaseDto> GetTopPurchases(int topN)
    {
        return _context.Purchases
            .OrderByDescending(p => p.TotalPrice)
            .Take(topN)
            .Select(p => new PurchaseDto
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
