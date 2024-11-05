namespace ShopSalesManagement.Api.DTOs;

public class PurchaseCreateDto
{
    public int SaleId { get; set; }  
    public int ProductId { get; set; } 
    public int Quantity { get; set; }  
    public decimal UnitPrice { get; set; }  
    public decimal TotalPrice { get; set; } 
}
