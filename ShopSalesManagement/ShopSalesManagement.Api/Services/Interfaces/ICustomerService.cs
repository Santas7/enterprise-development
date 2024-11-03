using ShopSalesManagement.Api.DTOs;

namespace ShopSalesManagement.Api.Services;

public interface ICustomerService
{
    IEnumerable<CustomerDTO> GetAll();
    CustomerDTO? GetById(int id);
    CustomerDTO Create(CustomerDTO customerDto);
    bool Update(int id, CustomerDTO customerDto);
    bool Delete(int id);
}
