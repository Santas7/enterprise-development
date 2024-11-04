namespace ShopSalesManagement.Api.DTOs;

public class PurchaseDto
{
    public int Id { get; set; }
    public int SaleId { get; set; }  
    public int ProductId { get; set; } 
    public int Quantity { get; set; }  
    public decimal UnitPrice { get; set; }  
    public decimal TotalPrice { get; set; } 
}
