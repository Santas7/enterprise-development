using Microsoft.AspNetCore.Mvc;
using ShopSalesManagement.Api.DTOs;
using ShopSalesManagement.Api.Services.Interfaces;
using System.Collections.Generic;

namespace ShopSalesManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StoreController : ControllerBase
{
    private readonly IStoreService _storeService;

    public StoreController(IStoreService storeService)
    {
        _storeService = storeService;
    }

    /// <summary>
    /// Получить все магазины.
    /// </summary>
    /// <returns>Список всех магазинов.</returns>
    [HttpGet]
    public ActionResult<IEnumerable<StoreDto>> GetStores()
    {
        var stores = _storeService.GetAll();
        return Ok(stores);
    }

    /// <summary>
    /// Получить магазин по ID.
    /// </summary>
    /// <param name="id">Идентификатор магазина.</param>
    /// <returns>Магазин с указанным ID или статус 404, если магазин не найден.</returns>
    [HttpGet("{id}")]
    public ActionResult<StoreDto> GetStore(int id)
    {
        var store = _storeService.GetById(id);
        if (store == null) return NotFound();
        return Ok(store);
    }

    /// <summary>
    /// Создать новый магазин.
    /// </summary>
    /// <param name="storeDto">Данные для создания магазина.</param>
    /// <returns>Созданный магазин с статусом 201.</returns>
    [HttpPost]
    public ActionResult<StoreDto> CreateStore([FromBody] StoreDto storeDto)
    {
        var store = _storeService.Create(storeDto);
        return CreatedAtAction(nameof(GetStore), new { id = store.Id }, store);
    }

    /// <summary>
    /// Обновить магазин по ID.
    /// </summary>
    /// <param name="id">Идентификатор магазина, который нужно обновить.</param>
    /// <param name="storeDto">Новые данные для обновления магазина.</param>
    /// <returns>Статус 204, если обновление прошло успешно, или статус 404, если магазин не найден.</returns>
    [HttpPut("{id}")]
    public IActionResult UpdateStore(int id, [FromBody] StoreDto storeDto)
    {
        if (!_storeService.Update(id, storeDto)) return NotFound();
        return NoContent();
    }

    /// <summary>
    /// Удалить магазин по ID.
    /// </summary>
    /// <param name="id">Идентификатор магазина, который нужно удалить.</param>
    /// <returns>Статус 204, если удаление прошло успешно, или статус 404, если магазин не найден.</returns>
    [HttpDelete("{id}")]
    public IActionResult DeleteStore(int id)
    {
        if (!_storeService.Delete(id)) return NotFound();
        return NoContent();
    }

    /// <summary>
    /// Получить все продукты в магазине по ID.
    /// </summary>
    /// <param name="storeId">Идентификатор магазина.</param>
    /// <returns>Список продуктов в указанном магазине.</returns>
    [HttpGet("{storeId}/products")]
    public ActionResult<IEnumerable<ProductDto>> GetProductsInStore(int storeId)
    {
        var products = _storeService.GetProductsByStoreId(storeId);
        return Ok(products);
    }

    /// <summary>
    /// Получить среднюю цену продуктов по товарным группам.
    /// </summary>
    /// <returns>Словарь, содержащий средние цены по товарным группам.</returns>
    [HttpGet("average-price-by-group")]
    public ActionResult<Dictionary<int, Dictionary<int, decimal>>> GetAveragePriceByGroup()
    {
        var averages = _storeService.GetAveragePriceByProductGroup();
        return Ok(averages);
    }
}
