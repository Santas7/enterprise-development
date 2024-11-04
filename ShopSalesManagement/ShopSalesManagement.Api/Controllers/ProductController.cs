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

    /// <summary>
    /// Получить все товары.
    /// </summary>
    /// <returns>Список товаров.</returns>
    [HttpGet]
    public ActionResult<IEnumerable<ProductDto>> GetProducts()
    {
        var products = _productService.GetAll();
        return Ok(products);
    }

    /// <summary>
    /// Получить товар по ID.
    /// </summary>
    /// <param name="id">Идентификатор товара.</param>
    /// <returns>Товар с указанным ID или статус 404, если товар не найден.</returns>
    [HttpGet("{id}")]
    public ActionResult<ProductDto> GetProduct(int id)
    {
        var product = _productService.GetById(id);
        if (product == null) return NotFound();
        return Ok(product);
    }

    /// <summary>
    /// Создать новый товар.
    /// </summary>
    /// <param name="productDto">Данные товара для создания.</param>
    /// <returns>Созданный товар с статусом 201.</returns>
    [HttpPost]
    public ActionResult<ProductDto> CreateProduct([FromBody] ProductDto productDto)
    {
        var product = _productService.Create(productDto);
        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
    }

    /// <summary>
    /// Обновить товар по ID.
    /// </summary>
    /// <param name="id">Идентификатор товара, который нужно обновить.</param>
    /// <param name="productDto">Новые данные товара.</param>
    /// <returns>Статус 204, если обновление прошло успешно, или статус 404, если товар не найден.</returns>
    [HttpPut("{id}")]
    public IActionResult UpdateProduct(int id, [FromBody] ProductDto productDto)
    {
        if (!_productService.Update(id, productDto)) return NotFound();
        return NoContent();
    }

    /// <summary>
    /// Удалить товар по ID.
    /// </summary>
    /// <param name="id">Идентификатор товара, который нужно удалить.</param>
    /// <returns>Статус 204, если удаление прошло успешно, или статус 404, если товар не найден.</returns>
    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(int id)
    {
        if (!_productService.Delete(id)) return NotFound();
        return NoContent();
    }

    /// <summary>
    /// Получить магазины, где доступен указанный товар.
    /// </summary>
    /// <param name="productId">Идентификатор товара.</param>
    /// <returns>Список магазинов, где доступен товар.</returns>
    [HttpGet("{productId}/stores")]
    public ActionResult<IEnumerable<StoreDto>> GetStoresForProduct(int productId)
    {
        var stores = _productService.GetStoresWithProduct(productId);
        return Ok(stores);
    }
}
