﻿using System.ComponentModel.DataAnnotations;

namespace ShopSalesManagement.Domain;

public class ProductGroup
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; } = string.Empty;

    public ICollection<Product> Products { get; set; } = new List<Product>();

    public ProductGroup(string name) 
    {
        Name = name;
    }
    public ProductGroup() { }
}
