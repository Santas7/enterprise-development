using ShopSalesManagement.Api.DTOs;

namespace ShopSalesManagement.Api.Services;

public interface ICustomerService
{
    IEnumerable<CustomerDto> GetAll();
    CustomerDto? GetById(int id);
    CustomerDto Create(CustomerDto customerDto);
    bool Update(int id, CustomerDto customerDto);
    bool Delete(int id);
}
