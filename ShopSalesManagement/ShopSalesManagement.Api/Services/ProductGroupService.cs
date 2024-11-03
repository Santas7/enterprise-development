using ShopSalesManagement.Api.DTOs;
using ShopSalesManagement.Domain;

using System.Collections.Generic;
using System.Linq;

namespace ShopSalesManagement.Api.Services
{
    public class ProductGroupService : IProductGroupService
    {
        private readonly ApplicationDbContext _context;

        public ProductGroupService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ProductGroupDTO> GetAll()
        {
            return _context.ProductGroups.Select(pg => new ProductGroupDTO
            {
                Id = pg.Id,
                Name = pg.Name
            }).ToList();
        }

        public ProductGroupDTO GetById(int id)
        {
            var productGroup = _context.ProductGroups.Find(id);
            return productGroup == null ? null : new ProductGroupDTO
            {
                Id = productGroup.Id,
                Name = productGroup.Name
            };
        }

        public ProductGroupDTO Create(ProductGroupDTO productGroupDto)
        {
            var productGroup = new ProductGroup
            {
                Name = productGroupDto.Name
            };
            _context.ProductGroups.Add(productGroup);
            _context.SaveChanges();
            productGroupDto.Id = productGroup.Id;
            return productGroupDto;
        }

        public bool Update(int id, ProductGroupDTO productGroupDto)
        {
            var productGroup = _context.ProductGroups.Find(id);
            if (productGroup == null) return false;

            productGroup.Name = productGroupDto.Name;
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var productGroup = _context.ProductGroups.Find(id);
            if (productGroup == null) return false;

            _context.ProductGroups.Remove(productGroup);
            _context.SaveChanges();
            return true;
        }
    }
}
