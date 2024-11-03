namespace ShopSalesManagement.Api.DTOs
{
    public class StockDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; } 
        public int StoreId { get; set; } 
        public int Quantity { get; set; }  
    }
}
