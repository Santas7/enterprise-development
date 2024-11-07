using System.ComponentModel.DataAnnotations;

namespace ShopSalesManagement.Domain;

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

    public ProductGroup? ProductGroup { get; set; }
    public Product(string barcode, string name, string type, decimal price, DateTime expirationDate)
    {
        Barcode = barcode;
        Name = name;
        Type = type;
        Price = price;
        ExpirationDate = expirationDate;
    }
}
