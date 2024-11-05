using Microsoft.AspNetCore.Mvc;
using ShopSalesManagement.Api.DTOs;
using ShopSalesManagement.Api.Services.Interfaces;

namespace ShopSalesManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductGroupController : ControllerBase
{
    private readonly IProductGroupService _productGroupService;

    public ProductGroupController(IProductGroupService productGroupService)
    {
        _productGroupService = productGroupService;
    }

    /// <summary>
    /// Получить все товарные группы.
    /// </summary>
    /// <returns>Список товарных групп.</returns>
    [HttpGet]
    public ActionResult<IEnumerable<ProductGroupDto>> GetProductGroups()
    {
        var productGroups = _productGroupService.GetAll();
        return Ok(productGroups);
    }

    /// <summary>
    /// Получить товарную группу по ID.
    /// </summary>
    /// <param name="id">Идентификатор товарной группы.</param>
    /// <returns>Товарная группа с указанным ID или статус 404, если группа не найдена.</returns>
    [HttpGet("{id}")]
    public ActionResult<ProductGroupDto> GetProductGroup(int id)
    {
        var productGroup = _productGroupService.GetById(id);
        if (productGroup == null) return NotFound();
        return Ok(productGroup);
    }

    /// <summary>
    /// Создать новую товарную группу.
    /// </summary>
    /// <param name="productGroupDto">Данные товарной группы для создания.</param>
    /// <returns>Созданная товарная группа с статусом 201.</returns>
    [HttpPost]
    public ActionResult<ProductGroupDto> CreateProductGroup([FromBody] ProductGroupCreateDto productGroupCreateDto)
    {
        var productGroup = _productGroupService.Create(productGroupCreateDto);
        return CreatedAtAction(nameof(GetProductGroup), new { id = productGroup.Id }, productGroup);
    }

    /// <summary>
    /// Обновить товарную группу по ID.
    /// </summary>
    /// <param name="id">Идентификатор товарной группы, которую нужно обновить.</param>
    /// <param name="productGroupDto">Новые данные товарной группы.</param>
    /// <returns>Статус 204, если обновление прошло успешно, или статус 404, если группа не найдена.</returns>
    [HttpPut("{id}")]
    public IActionResult UpdateProductGroup(int id, [FromBody] ProductGroupDto productGroupDto)
    {
        if (!_productGroupService.Update(id, productGroupDto)) return NotFound();
        return NoContent();
    }

    /// <summary>
    /// Удалить товарную группу по ID.
    /// </summary>
    /// <param name="id">Идентификатор товарной группы, которую нужно удалить.</param>
    /// <returns>Статус 204, если удаление прошло успешно, или статус 404, если группа не найдена.</returns>
    [HttpDelete("{id}")]
    public IActionResult DeleteProductGroup(int id)
    {
        if (!_productGroupService.Delete(id)) return NotFound();
        return NoContent();
    }
}
