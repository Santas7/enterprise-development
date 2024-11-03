using ShopSalesManagement.Api.DTOs;
using System.Collections.Generic;

namespace ShopSalesManagement.Api.Services;

public interface IProductGroupService
{
    IEnumerable<ProductGroupDTO> GetAll();
    ProductGroupDTO GetById(int id);
    ProductGroupDTO Create(ProductGroupDTO productGroupDto);
    bool Update(int id, ProductGroupDTO productGroupDto);
    bool Delete(int id);
}
