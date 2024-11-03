using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopSalesManagement.Domain
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }
        public DateTime SaleDate { get; set; }
        public int CustomerId { get; set; }
        public int StoreId { get; set; }
        public decimal TotalAmount { get; set; }

        public Customer Customer { get; set; }
        public Store Store { get; set; }
        public ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
    }
}
