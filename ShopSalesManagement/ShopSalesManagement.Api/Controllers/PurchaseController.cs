using Microsoft.AspNetCore.Mvc;
using ShopSalesManagement.Api.DTOs;
using ShopSalesManagement.Api.Services;
using System.Collections.Generic;

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

    [HttpGet]
    public ActionResult<IEnumerable<PurchaseDTO>> GetPurchases()
    {
        var purchases = _purchaseService.GetAll();
        return Ok(purchases);
    }

    [HttpGet("{id}")]
    public ActionResult<PurchaseDTO> GetPurchase(int id)
    {
        var purchase = _purchaseService.GetById(id);
        if (purchase == null) return NotFound();
        return Ok(purchase);
    }

    [HttpPost]
    public ActionResult<PurchaseDTO> CreatePurchase(PurchaseDTO purchaseDto)
    {
        var purchase = _purchaseService.Create(purchaseDto);
        return CreatedAtAction(nameof(GetPurchase), new { id = purchase.Id }, purchase);
    }

    [HttpPut("{id}")]
    public IActionResult UpdatePurchase(int id, PurchaseDTO purchaseDto)
    {
        if (!_purchaseService.Update(id, purchaseDto)) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeletePurchase(int id)
    {
        if (!_purchaseService.Delete(id)) return NotFound();
        return NoContent();
    }

    [HttpGet("top/{topN}")]
    public ActionResult<IEnumerable<PurchaseDTO>> GetTopPurchases(int topN)
    {
        var purchases = _purchaseService.GetTopPurchases(topN);
        return Ok(purchases);
    }
}
