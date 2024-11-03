using Microsoft.AspNetCore.Mvc;
using ShopSalesManagement.Api.DTOs;
using ShopSalesManagement.Api.Services;
using System.Collections.Generic;

namespace ShopSalesManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<SaleDTO>> GetSales()
        {
            var sales = _saleService.GetAll();
            return Ok(sales);
        }

        [HttpGet("{id}")]
        public ActionResult<SaleDTO> GetSale(int id)
        {
            var sale = _saleService.GetById(id);
            if (sale == null) return NotFound();
            return Ok(sale);
        }

        [HttpPost]
        public ActionResult<SaleDTO> CreateSale(SaleDTO saleDto)
        {
            var sale = _saleService.Create(saleDto);
            return CreatedAtAction(nameof(GetSale), new { id = sale.Id }, sale);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSale(int id, SaleDTO saleDto)
        {
            if (!_saleService.Update(id, saleDto)) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSale(int id)
        {
            if (!_saleService.Delete(id)) return NotFound();
            return NoContent();
        }

        [HttpGet("stores/sales-above/{threshold}")]
        public ActionResult<IEnumerable<StoreDTO>> GetStoresWithSalesAbove(decimal threshold)
        {
            var startDate = DateTime.Now.AddMonths(-1);
            var endDate = DateTime.Now;
            var stores = _saleService.GetStoresWithSalesAbove(threshold, startDate, endDate);
            return Ok(stores);
        }
    }
}
