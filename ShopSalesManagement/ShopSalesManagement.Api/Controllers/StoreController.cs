using Microsoft.AspNetCore.Mvc;
using ShopSalesManagement.Api.DTOs;
using ShopSalesManagement.Api.Services;
using System.Collections.Generic;

namespace ShopSalesManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;

        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<StoreDTO>> GetStores()
        {
            var stores = _storeService.GetAll();
            return Ok(stores);
        }

        [HttpGet("{id}")]
        public ActionResult<StoreDTO> GetStore(int id)
        {
            var store = _storeService.GetById(id);
            if (store == null) return NotFound();
            return Ok(store);
        }

        [HttpPost]
        public ActionResult<StoreDTO> CreateStore(StoreDTO storeDto)
        {
            var store = _storeService.Create(storeDto);
            return CreatedAtAction(nameof(GetStore), new { id = store.Id }, store);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStore(int id, StoreDTO storeDto)
        {
            if (!_storeService.Update(id, storeDto)) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStore(int id)
        {
            if (!_storeService.Delete(id)) return NotFound();
            return NoContent();
        }

        [HttpGet("{storeId}/products")]
        public ActionResult<IEnumerable<ProductDTO>> GetProductsInStore(int storeId)
        {
            var products = _storeService.GetProductsByStoreId(storeId);
            return Ok(products);
        }

        [HttpGet("average-price-by-group")]
        public ActionResult<Dictionary<int, Dictionary<int, decimal>>> GetAveragePriceByGroup()
        {
            var averages = _storeService.GetAveragePriceByProductGroup();
            return Ok(averages);
        }
    }
}
