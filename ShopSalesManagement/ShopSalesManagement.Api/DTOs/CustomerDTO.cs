namespace ShopSalesManagement.Api.DTOs;

public class CustomerDto
{
    public int Id { get; set; }
    public string CardNumber { get; set; } = string.Empty; 
    public string FullName { get; set; } = string.Empty; 
}
