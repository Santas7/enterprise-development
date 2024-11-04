﻿using Microsoft.AspNetCore.Mvc;
using ShopSalesManagement.Api.DTOs;
using ShopSalesManagement.Api.Services;
using System.Collections.Generic;

namespace ShopSalesManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StockController : ControllerBase
{
    private readonly IStockService _stockService;

    public StockController(IStockService stockService)
    {
        _stockService = stockService;
    }

    /// <summary>
    /// Получить все записи о наличии товара.
    /// </summary>
    /// <returns>Список всех записей о наличии товара.</returns>
    [HttpGet]
    public ActionResult<IEnumerable<StockDTO>> GetStocks()
    {
        var stocks = _stockService.GetAll();
        return Ok(stocks);
    }

    /// <summary>
    /// Получить запись о наличии товара по ID.
    /// </summary>
    /// <param name="id">Идентификатор записи о наличии товара.</param>
    /// <returns>Запись о наличии товара с указанным ID или статус 404, если запись не найдена.</returns>
    [HttpGet("{id}")]
    public ActionResult<StockDTO> GetStock(int id)
    {
        var stock = _stockService.GetById(id);
        if (stock == null) return NotFound();
        return Ok(stock);
    }

    /// <summary>
    /// Создать новую запись о наличии товара.
    /// </summary>
    /// <param name="stockDto">Данные для создания записи о наличии товара.</param>
    /// <returns>Созданная запись о наличии товара с статусом 201.</returns>
    [HttpPost]
    public ActionResult<StockDTO> CreateStock([FromBody] StockDTO stockDto)
    {
        var stock = _stockService.Create(stockDto);
        return CreatedAtAction(nameof(GetStock), new { id = stock.Id }, stock);
    }

    /// <summary>
    /// Обновить запись о наличии товара по ID.
    /// </summary>
    /// <param name="id">Идентификатор записи о наличии товара, которую нужно обновить.</param>
    /// <param name="stockDto">Новые данные для обновления записи о наличии товара.</param>
    /// <returns>Статус 204, если обновление прошло успешно, или статус 404, если запись не найдена.</returns>
    [HttpPut("{id}")]
    public IActionResult UpdateStock(int id, [FromBody] StockDTO stockDto)
    {
        if (!_stockService.Update(id, stockDto)) return NotFound();
        return NoContent();
    }

    /// <summary>
    /// Удалить запись о наличии товара по ID.
    /// </summary>
    /// <param name="id">Идентификатор записи о наличии товара, которую нужно удалить.</param>
    /// <returns>Статус 204, если удаление прошло успешно, или статус 404, если запись не найдена.</returns>
    [HttpDelete("{id}")]
    public IActionResult DeleteStock(int id)
    {
        if (!_stockService.Delete(id)) return NotFound();
        return NoContent();
    }

    /// <summary>
    /// Получить все истекшие продукты.
    /// </summary>
    /// <returns>Список истекших продуктов.</returns>
    [HttpGet("expired")]
    public ActionResult<IEnumerable<ProductDTO>> GetExpiredProducts()
    {
        var expiredProducts = _stockService.GetExpiredProducts();
        return Ok(expiredProducts);
    }
}
