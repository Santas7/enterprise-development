using Microsoft.AspNetCore.Mvc;
using ShopSalesManagement.Api.DTOs;
using ShopSalesManagement.Api.Services;
using System.Collections.Generic;

namespace ShopSalesManagement.Api.Controllers
{
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
        /// Получить всех клиентов.
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<CustomerDTO>> GetCustomers()
        {
            var customers = _customerService.GetAll();
            return Ok(customers);
        }

        /// <summary>
        /// Получить клиента по ID.
        /// </summary>
        [HttpGet("{id}")]
        public ActionResult<CustomerDTO> GetCustomer(int id)
        {
            var customer = _customerService.GetById(id);
            if (customer == null) return NotFound();
            return Ok(customer);
        }

        /// <summary>
        /// Создать нового клиента.
        /// </summary>
        [HttpPost]
        public ActionResult<CustomerDTO> CreateCustomer(CustomerDTO customerDto)
        {
            var customer = _customerService.Create(customerDto);
            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
        }

        /// <summary>
        /// Обновить клиента по ID.
        /// </summary>
        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, CustomerDTO customerDto)
        {
            if (!_customerService.Update(id, customerDto)) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Удалить клиента по ID.
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            if (!_customerService.Delete(id)) return NotFound();
            return NoContent();
        }
    }
}
