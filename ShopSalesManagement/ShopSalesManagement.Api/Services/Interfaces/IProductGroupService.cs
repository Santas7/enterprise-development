using ShopSalesManagement.Api.DTOs;

namespace ShopSalesManagement.Api.Services.Interfaces;

public interface IProductGroupService
{
    IEnumerable<ProductGroupDto> GetAll();
    ProductGroupDto? GetById(int id);
    ProductGroupDto Create(ProductGroupDto productGroupDto);
    bool Update(int id, ProductGroupDto productGroupDto);
    bool Delete(int id);
}
