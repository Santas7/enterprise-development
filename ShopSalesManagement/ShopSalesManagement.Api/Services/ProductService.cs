using ShopSalesManagement.Api.DTOs;
using ShopSalesManagement.Api.Services.Interfaces;
using ShopSalesManagement.Domain;

namespace ShopSalesManagement.Api.Services;

public class ProductService : IProductService 
{
    private readonly ApplicationDbContext _context;

    public ProductService(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<ProductDto> GetAll()
    {
        return _context.Products
            .Select(p => new ProductDto { Id = p.Id, Name = p.Name, Price = p.Price })
            .ToList();
    }

    public ProductDto? GetById(int id)
    {
        var product = _context.Products.Find(id);
        if (product == null) return null;

        return new ProductDto { Id = product.Id, Name = product.Name, Price = product.Price };
    }

    public ProductDto Create(ProductCreateDto productCreateDto)
    {
        var product = new Product(
            productCreateDto.Barcode,
            productCreateDto.Name,
            productCreateDto.Type,
            productCreateDto.Price,
            productCreateDto.ExpirationDate
        );

        _context.Products.Add(product);
        _context.SaveChanges();

        return new ProductDto
        {
            Id = product.Id,
            Barcode = product.Barcode,
            ProductGroupId = product.ProductGroupId,
            Name = product.Name,
            PackageWeight = product.Weight,
            Type = product.Type,
            Price = product.Price,
            ExpirationDate = product.ExpirationDate
        };
    }

    public bool Update(int id, ProductDto productDto)
    {
        var product = _context.Products.Find(id);
        if (product == null) return false;

        product.Name = productDto.Name;
        product.Price = productDto.Price;
        _context.SaveChanges();

        return true;
    }

    public bool Delete(int id)
    {
        var product = _context.Products.Find(id);
        if (product == null) return false;

        _context.Products.Remove(product);
        _context.SaveChanges();

        return true;
    }

    public IEnumerable<StoreDto> GetStoresWithProduct(int productId)
    {
        return _context.Stocks
            .Where(s => s.ProductId == productId)
            .Select(s => new StoreDto { Id = s.Store!.Id, Name = s.Store.Name ?? string.Empty })
            .Distinct()
            .ToList();
    }
}
