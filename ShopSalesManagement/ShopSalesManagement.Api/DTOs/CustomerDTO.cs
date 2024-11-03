namespace ShopSalesManagement.Api.DTOs;

public class CustomerDTO
{
    public int Id { get; set; }
    public string CardNumber { get; set; } = string.Empty; 
    public string FullName { get; set; } = string.Empty; 
}
