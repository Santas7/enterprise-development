namespace ShopSalesManagement.Api.DTOs;

public class StockCreateDto
{
    public int ProductId { get; set; } 
    public int StoreId { get; set; } 
    public int Quantity { get; set; }  
}
