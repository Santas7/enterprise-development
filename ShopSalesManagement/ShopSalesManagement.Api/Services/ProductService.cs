using ShopSalesManagement.Api.DTOs;
using ShopSalesManagement.Domain;
using System.Collections.Generic;
using System.Linq;

public class ProductService
{
    private readonly ApplicationDbContext _context;

    public ProductService(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<ProductDTO> GetAll()
    {
        return _context.Products
            .Select(p => new ProductDTO { Id = p.Id, Name = p.Name, Price = p.Price })
            .ToList();
    }

    public ProductDTO GetById(int id)
    {
        var product = _context.Products.Find(id);
        if (product == null) return null;

        return new ProductDTO { Id = product.Id, Name = product.Name, Price = product.Price };
    }

    public ProductDTO Create(ProductDTO productDto)
    {
        var product = new Product(
            productDto.Barcode,        
            productDto.Name,          
            productDto.Type,          
            productDto.Price,         
            productDto.ExpirationDate 
        );

        _context.Products.Add(product);
        _context.SaveChanges();

        productDto.Id = product.Id;
        return productDto;
    }


    public bool Update(int id, ProductDTO productDto)
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

    public IEnumerable<StoreDTO> GetStoresWithProduct(int productId)
    {
        return _context.Stocks
            .Where(s => s.ProductId == productId)
            .Select(s => new StoreDTO { Id = s.Store.Id, Name = s.Store.Name })
            .Distinct()
            .ToList();
    }
}
