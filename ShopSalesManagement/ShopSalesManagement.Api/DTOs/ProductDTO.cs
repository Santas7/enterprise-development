namespace ShopSalesManagement.Api.DTOs;

public class ProductDTO
{
    public int Id { get; set; }
    public string Barcode { get; set; } = string.Empty;
    public int ProductGroupId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal PackageWeight { get; set; }
    public string Type { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public DateTime ExpirationDate { get; set; }
}
