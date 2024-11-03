using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ShopSalesManagement.Domain
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string FullName { get; set; }

        public ICollection<Sale> Sales { get; set; } = new List<Sale>();

        public Customer(string cardNumber, string fullName)
        {
            CardNumber = cardNumber;
            FullName = fullName;
        }
        public Customer() { }
    }
}
