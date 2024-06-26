﻿namespace POS.Domain.Entities;

public partial class Product : BaseEntity
{
    public Product() 
    {
     // error??   SaleItems = new HashSet<SaleItem>();
    }
    
    public string Description { get; set; } = null!;

    public string? Brand { get; set; } = null!;

    public decimal PurchaseCost { get; set; }

    public decimal SalesCost { get; set; }

    public string? UrlImage { get; set; }

    public int CategoryId { get; set; }
    public string? Sku { get; set; }
    public virtual Category Category { get; set; } = null!;
    //?????error???? public virtual ICollection<SaleItem> SaleItems { get; set; } = new List<SaleItem>();


    // Remove the following line
    // public virtual ICollection<SaleItem> SaleItems { get; set; } = new List<SaleItem>();
}


