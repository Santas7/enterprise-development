using Microsoft.AspNetCore.Mvc;
using ShopSalesManagement.Api.DTOs;
using ShopSalesManagement.Api.Services.Interfaces;

namespace ShopSalesManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SaleController : ControllerBase
{
    private readonly ISaleService _saleService;

    public SaleController(ISaleService saleService)
    {
        _saleService = saleService;
    }

    /// <summary>
    /// Получить все продажи.
    /// </summary>
    /// <returns>Список всех продаж.</returns>
    [HttpGet]
    public ActionResult<IEnumerable<SaleDto>> GetSales()
    {
        var sales = _saleService.GetAll();
        return Ok(sales);
    }

    /// <summary>
    /// Получить продажу по ID.
    /// </summary>
    /// <param name="id">Идентификатор продажи.</param>
    /// <returns>Продажа с указанным ID или статус 404, если продажа не найдена.</returns>
    [HttpGet("{id}")]
    public ActionResult<SaleDto> GetSale(int id)
    {
        var sale = _saleService.GetById(id);
        if (sale == null) return NotFound();
        return Ok(sale);
    }

    /// <summary>
    /// Создать новую продажу.
    /// </summary>
    /// <param name="saleDto">Данные для создания продажи.</param>
    /// <returns>Созданная продажа с статусом 201.</returns>
    [HttpPost]
    public ActionResult<SaleDto> CreateSale([FromBody] SaleDto saleDto)
    {
        var sale = _saleService.Create(saleDto);
        return CreatedAtAction(nameof(GetSale), new { id = sale.Id }, sale);
    }

    /// <summary>
    /// Обновить продажу по ID.
    /// </summary>
    /// <param name="id">Идентификатор продажи, которую нужно обновить.</param>
    /// <param name="saleDto">Новые данные продажи.</param>
    /// <returns>Статус 204, если обновление прошло успешно, или статус 404, если продажа не найдена.</returns>
    [HttpPut("{id}")]
    public IActionResult UpdateSale(int id, [FromBody] SaleDto saleDto)
    {
        if (!_saleService.Update(id, saleDto)) return NotFound();
        return NoContent();
    }

    /// <summary>
    /// Удалить продажу по ID.
    /// </summary>
    /// <param name="id">Идентификатор продажи, которую нужно удалить.</param>
    /// <returns>Статус 204, если удаление прошло успешно, или статус 404, если продажа не найдена.</returns>
    [HttpDelete("{id}")]
    public IActionResult DeleteSale(int id)
    {
        if (!_saleService.Delete(id)) return NotFound();
        return NoContent();
    }

    /// <summary>
    /// Получить магазины с продажами выше заданного порога.
    /// </summary>
    /// <param name="threshold">Минимальная сумма продаж для фильтрации магазинов.</param>
    /// <returns>Список магазинов с продажами выше указанного порога.</returns>
    [HttpGet("stores/sales-above/{threshold}")]
    public ActionResult<IEnumerable<StoreDto>> GetStoresWithSalesAbove(decimal threshold)
    {
        var startDate = DateTime.Now.AddMonths(-1);
        var endDate = DateTime.Now;
        var stores = _saleService.GetStoresWithSalesAbove(threshold, startDate, endDate);
        return Ok(stores);
    }
}
