namespace ShopSalesManagement.Api.DTOs;

public class SaleDto
{
    public int Id { get; set; }
    public DateTime SaleDate { get; set; } 
    public int CustomerId { get; set; }  
    public int StoreId { get; set; } 
    public decimal TotalAmount { get; set; }  
}
