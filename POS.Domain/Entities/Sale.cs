using POS.Domain.Entities;

namespace POS.Domain.Entities;

public partial class Sale :BaseEntity
{
    public Sale() 
    {
        Payments = new HashSet<Payment>();
        SaleItems = new HashSet<SaleItem>();
    }
    
    public int CustomerId { get; set; }
    public decimal SubTotal { get; set; }
    public decimal Total { get; set; }
    public decimal Balance { get; set; }
    public bool Closed { get; set; }
    public bool Paid { get; set; }
    public virtual Customer Customer { get; set; } = null!;
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
    public virtual ICollection<SaleItem> SaleItems { get; set; } = new List<SaleItem>();

    
}
