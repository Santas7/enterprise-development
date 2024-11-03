using System.ComponentModel.DataAnnotations;
using System;

namespace ShopSalesManagement.Domain
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Barcode { get; set; }
        public int ProductGroupId { get; set; }
        public string Name { get; set; }
        public decimal Weight { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public DateTime ExpirationDate { get; set; }
        public ProductGroup ProductGroup { get; set; }
    }
}
