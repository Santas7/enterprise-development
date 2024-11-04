using ShopSalesManagement.Api.DTOs;
using ShopSalesManagement.Domain;

using System.Collections.Generic;
using System.Linq;

namespace ShopSalesManagement.Api.Services;

public class ProductGroupService : IProductGroupService
{
    private readonly ApplicationDbContext _context;

    public ProductGroupService(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<ProductGroupDto> GetAll()
    {
        return _context.ProductGroups.Select(pg => new ProductGroupDto
        {
            Id = pg.Id,
            Name = pg.Name ?? string.Empty
        }).ToList();
    }

    public ProductGroupDto? GetById(int id)
    {
        var productGroup = _context.ProductGroups.Find(id);
        return productGroup == null ? null : new ProductGroupDto
        {
            Id = productGroup.Id,
            Name = productGroup.Name ?? string.Empty 
        };
    }


    public ProductGroupDto Create(ProductGroupDto productGroupDto)
    {
        var productGroup = new ProductGroup
        {
            Name = productGroupDto.Name
        };
        _context.ProductGroups.Add(productGroup);
        _context.SaveChanges();
        productGroupDto.Id = productGroup.Id;
        return productGroupDto;
    }

    public bool Update(int id, ProductGroupDto productGroupDto)
    {
        var productGroup = _context.ProductGroups.Find(id);
        if (productGroup == null) return false;

        productGroup.Name = productGroupDto.Name;
        _context.SaveChanges();
        return true;
    }

    public bool Delete(int id)
    {
        var productGroup = _context.ProductGroups.Find(id);
        if (productGroup == null) return false;

        _context.ProductGroups.Remove(productGroup);
        _context.SaveChanges();
        return true;
    }
}
