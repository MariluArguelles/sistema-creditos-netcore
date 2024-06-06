namespace POS.Domain.Entities;

public partial class Customer : BaseEntity
{
    public Customer() 
    {
        //Payments = new HashSet<Payment>();//error, no desaparecia la columna CustomerId en Payments
        Sales = new HashSet<Sale>();
    }
    
    public string Name { get; set; } = null!;

    public string LastName1 { get; set; } = null!;

    public string LastName2 { get; set; } = null!;

    public DateTime? BirthDate { get; set; }

    public string? Gender { get; set; }

    public string? Email { get; set; }

    // public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();//error, no desaparecia la columna CustomerId en Payments

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
