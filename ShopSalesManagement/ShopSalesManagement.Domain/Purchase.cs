using System.ComponentModel.DataAnnotations;

namespace ShopSalesManagement.Domain
{
    public class Purchase
    {
        [Key]
        public int Id { get; set; }
        public int SaleId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public required Sale Sale { get; set; }
        public required Product Product { get; set; }
    }
}
