using Microsoft.AspNetCore.Mvc;
using ShopSalesManagement.Api.DTOs;
using ShopSalesManagement.Api.Services;
using System.Collections.Generic;

namespace ShopSalesManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;

        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<StockDTO>> GetStocks()
        {
            var stocks = _stockService.GetAll();
            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public ActionResult<StockDTO> GetStock(int id)
        {
            var stock = _stockService.GetById(id);
            if (stock == null) return NotFound();
            return Ok(stock);
        }

        [HttpPost]
        public ActionResult<StockDTO> CreateStock(StockDTO stockDto)
        {
            var stock = _stockService.Create(stockDto);
            return CreatedAtAction(nameof(GetStock), new { id = stock.Id }, stock);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStock(int id, StockDTO stockDto)
        {
            if (!_stockService.Update(id, stockDto)) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStock(int id)
        {
            if (!_stockService.Delete(id)) return NotFound();
            return NoContent();
        }

        [HttpGet("expired")]
        public ActionResult<IEnumerable<ProductDTO>> GetExpiredProducts()
        {
            var expiredProducts = _stockService.GetExpiredProducts();
            return Ok(expiredProducts);
        }
    }
}
