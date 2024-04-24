using System.Collections.Generic;

public class ShoppingCart
{
    public int Id { get; set; }
    public int CustomerId { get; set; } // foreign key for Customer
    public Customer? Customer { get; set; } // Make Customer property nullable
    public List<Product> Products { get; set; } = new List<Product>();
}