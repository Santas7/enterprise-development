using Microsoft.AspNetCore.Mvc;
using ShopSalesManagement.Api.DTOs;
using ShopSalesManagement.Api.Services;
using System.Collections.Generic;

namespace ShopSalesManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    /// <summary>
    /// Получить всех покупателей.
    /// </summary>
    /// <returns>Список покупателей.</returns>
    [HttpGet]
    public ActionResult<IEnumerable<CustomerDto>> GetCustomers()
    {
        var customers = _customerService.GetAll();
        return Ok(customers);
    }

    /// <summary>
    /// Получить покупателя по ID.
    /// </summary>
    /// <param name="id">Идентификатор покупателя.</param>
    /// <returns>Покупатель с указанным ID или статус 404, если покупатель не найден.</returns>
    [HttpGet("{id}")]
    public ActionResult<CustomerDto> GetCustomer(int id)
    {
        var customer = _customerService.GetById(id);
        if (customer == null) return NotFound();
        return Ok(customer);
    }

    /// <summary>
    /// Создать нового покупателя.
    /// </summary>
    /// <param name="customerDto">Данные покупателя для создания.</param>
    /// <returns>Созданный покупатель с статусом 201.</returns>
    [HttpPost]
    public ActionResult<CustomerDto> CreateCustomer([FromBody] CustomerDto customerDto)
    {
        var customer = _customerService.Create(customerDto);
        return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
    }

    /// <summary>
    /// Обновить покупателя по ID.
    /// </summary>
    /// <param name="id">Идентификатор покупателя, которого нужно обновить.</param>
    /// <param name="customerDto">Новые данные покупателя.</param>
    /// <returns>Статус 204, если обновление прошло успешно, или статус 404, если покупатель не найден.</returns>
    [HttpPut("{id}")]
    public IActionResult UpdateCustomer(int id, [FromBody] CustomerDto customerDto)
    {
        if (!_customerService.Update(id, customerDto)) return NotFound();
        return NoContent();
    }

    /// <summary>
    /// Удалить покупателя по ID.
    /// </summary>
    /// <param name="id">Идентификатор покупателя, которого нужно удалить.</param>
    /// <returns>Статус 204, если удаление прошло успешно, или статус 404, если покупатель не найден.</returns>
    [HttpDelete("{id}")]
    public IActionResult DeleteCustomer(int id)
    {
        if (!_customerService.Delete(id)) return NotFound();
        return NoContent();
    }
}
