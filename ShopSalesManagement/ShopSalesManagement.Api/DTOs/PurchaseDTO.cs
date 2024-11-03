﻿namespace ShopSalesManagement.Api.DTOs
{
    public class PurchaseDTO
    {
        public int Id { get; set; }
        public int SaleId { get; set; }  
        public int ProductId { get; set; } 
        public int Quantity { get; set; }  
        public decimal UnitPrice { get; set; }  
        public decimal TotalPrice { get; set; } 
    }
}