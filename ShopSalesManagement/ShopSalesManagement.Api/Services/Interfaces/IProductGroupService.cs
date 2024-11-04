using ShopSalesManagement.Api.DTOs;
using System.Collections.Generic;

namespace ShopSalesManagement.Api.Services.Interfaces;

public interface IProductGroupService
{
    IEnumerable<ProductGroupDto> GetAll();
    ProductGroupDto? GetById(int id);
    ProductGroupDto Create(ProductGroupDto productGroupDto);
    bool Update(int id, ProductGroupDto productGroupDto);
    bool Delete(int id);
}
