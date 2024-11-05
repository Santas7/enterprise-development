using Microsoft.AspNetCore.Mvc;
using ShopSalesManagement.Api.DTOs;
using ShopSalesManagement.Api.Services.Interfaces;

namespace ShopSalesManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PurchaseController : ControllerBase
{
    private readonly IPurchaseService _purchaseService;

    public PurchaseController(IPurchaseService purchaseService)
    {
        _purchaseService = purchaseService;
    }

    /// <summary>
    /// Получить все покупки.
    /// </summary>
    /// <returns>Список всех покупок.</returns>
    [HttpGet]
    public ActionResult<IEnumerable<PurchaseDto>> GetPurchases()
    {
        var purchases = _purchaseService.GetAll();
        return Ok(purchases);
    }

    /// <summary>
    /// Получить покупку по ID.
    /// </summary>
    /// <param name="id">Идентификатор покупки.</param>
    /// <returns>Покупка с указанным ID или статус 404, если покупка не найдена.</returns>
    [HttpGet("{id}")]
    public ActionResult<PurchaseDto> GetPurchase(int id)
    {
        var purchase = _purchaseService.GetById(id);
        if (purchase == null) return NotFound();
        return Ok(purchase);
    }

    /// <summary>
    /// Создать новую покупку.
    /// </summary>
    /// <param name="purchaseDto">Данные для создания покупки.</param>
    /// <returns>Созданная покупка с статусом 201.</returns>
    [HttpPost]
    public ActionResult<PurchaseDto> CreatePurchase([FromBody] PurchaseCreateDto purchaseCreateDto)
    {
        var purchase = _purchaseService.Create(purchaseCreateDto);
        return CreatedAtAction(nameof(GetPurchase), new { id = purchase.Id }, purchase);
    }

    /// <summary>
    /// Обновить покупку по ID.
    /// </summary>
    /// <param name="id">Идентификатор покупки, которую нужно обновить.</param>
    /// <param name="purchaseDto">Новые данные покупки.</param>
    /// <returns>Статус 204, если обновление прошло успешно, или статус 404, если покупка не найдена.</returns>
    [HttpPut("{id}")]
    public IActionResult UpdatePurchase(int id, [FromBody] PurchaseDto purchaseDto)
    {
        if (!_purchaseService.Update(id, purchaseDto)) return NotFound();
        return NoContent();
    }

    /// <summary>
    /// Удалить покупку по ID.
    /// </summary>
    /// <param name="id">Идентификатор покупки, которую нужно удалить.</param>
    /// <returns>Статус 204, если удаление прошло успешно, или статус 404, если покупка не найдена.</returns>
    [HttpDelete("{id}")]
    public IActionResult DeletePurchase(int id)
    {
        if (!_purchaseService.Delete(id)) return NotFound();
        return NoContent();
    }

    /// <summary>
    /// Получить топ-N покупок.
    /// </summary>
    /// <param name="topN">Количество покупок для возврата.</param>
    /// <returns>Список топ-N покупок.</returns>
    [HttpGet("top/{topN}")]
    public ActionResult<IEnumerable<PurchaseDto>> GetTopPurchases(int topN)
    {
        var purchases = _purchaseService.GetTopPurchases(topN);
        return Ok(purchases);
    }
}
