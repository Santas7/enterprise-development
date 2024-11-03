using Microsoft.AspNetCore.Mvc;
using ShopSalesManagement.Api.DTOs;
using ShopSalesManagement.Api.Services;
using System.Collections.Generic;

namespace ShopSalesManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<ProductDTO>> GetProducts()
    {
        var products = _productService.GetAll();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public ActionResult<ProductDTO> GetProduct(int id)
    {
        var product = _productService.GetById(id);
        if (product == null) return NotFound();
        return Ok(product);
    }

    [HttpPost]
    public ActionResult<ProductDTO> CreateProduct(ProductDTO productDto)
    {
        var product = _productService.Create(productDto);
        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateProduct(int id, ProductDTO productDto)
    {
        if (!_productService.Update(id, productDto)) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(int id)
    {
        if (!_productService.Delete(id)) return NotFound();
        return NoContent();
    }

    [HttpGet("{productId}/stores")]
    public ActionResult<IEnumerable<StoreDTO>> GetStoresForProduct(int productId)
    {
        var stores = _productService.GetStoresWithProduct(productId);
        return Ok(stores);
    }
}
