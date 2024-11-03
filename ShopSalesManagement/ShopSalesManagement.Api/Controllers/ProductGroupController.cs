using Microsoft.AspNetCore.Mvc;
using ShopSalesManagement.Api.DTOs;
using ShopSalesManagement.Api.Services;
using System.Collections.Generic;

namespace ShopSalesManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductGroupController : ControllerBase
    {
        private readonly IProductGroupService _productGroupService;

        public ProductGroupController(IProductGroupService productGroupService)
        {
            _productGroupService = productGroupService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductGroupDTO>> GetProductGroups()
        {
            var productGroups = _productGroupService.GetAll();
            return Ok(productGroups);
        }

        [HttpGet("{id}")]
        public ActionResult<ProductGroupDTO> GetProductGroup(int id)
        {
            var productGroup = _productGroupService.GetById(id);
            if (productGroup == null) return NotFound();
            return Ok(productGroup);
        }

        [HttpPost]
        public ActionResult<ProductGroupDTO> CreateProductGroup(ProductGroupDTO productGroupDto)
        {
            var productGroup = _productGroupService.Create(productGroupDto);
            return CreatedAtAction(nameof(GetProductGroup), new { id = productGroup.Id }, productGroup);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProductGroup(int id, ProductGroupDTO productGroupDto)
        {
            if (!_productGroupService.Update(id, productGroupDto)) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProductGroup(int id)
        {
            if (!_productGroupService.Delete(id)) return NotFound();
            return NoContent();
        }
    }
}
