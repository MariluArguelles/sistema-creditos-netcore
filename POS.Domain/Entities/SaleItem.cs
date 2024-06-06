namespace POS.Domain.Entities;

public partial class SaleItem : BaseEntity
{
    public int SaleId { get; set; }
    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public virtual Sale Sale { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
