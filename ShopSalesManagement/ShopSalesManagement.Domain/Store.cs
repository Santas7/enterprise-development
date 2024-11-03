using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopSalesManagement.Domain
{
    public class Store
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public ICollection<Sale> Sales { get; set; } = new List<Sale>();
        public ICollection<Stock> Stocks { get; set; } = new List<Stock>();

        public Store(string name, string address)
        {
            Name = name;
            Address = address;
        }
        public Store() { }
    }
}
