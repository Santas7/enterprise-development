﻿using System.ComponentModel.DataAnnotations;

namespace ShopSalesManagement.Domain;

public class Customer
{
    [Key]
    public int Id { get; set; }
    public required string CardNumber { get; set; }
    public required string FullName { get; set; }

    public ICollection<Sale> Sales { get; set; } = new List<Sale>();

    public Customer(string cardNumber, string fullName)
    {
        CardNumber = cardNumber;
        FullName = fullName;
    }
    public Customer() { }
}
