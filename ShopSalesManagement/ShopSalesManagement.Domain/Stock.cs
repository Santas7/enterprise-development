using System.ComponentModel.DataAnnotations;

namespace ShopSalesManagement.Domain
{
    public class Stock
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int StoreId { get; set; }
        public int Quantity { get; set; }
        public Product Product { get; set; }
        public Store Store { get; set; }
    }
}
