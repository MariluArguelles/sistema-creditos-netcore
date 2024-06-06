namespace POS.Domain.Entities;

public partial class Payment : BaseEntity
{
    public int SaleId { get; set; }

    //cambie el tipo de datos de int a decimal para que pudiera guardar decimales, si no guardaba ceros
    public decimal Quantity { get; set; }

    public decimal Balance { get; set; }
    public DateTime PaymentDate { get; set; } //la fecha que agrega el usuario

    public string Description { get; set; } = null!;

    public virtual Sale Sale { get; set; } = null!;
    

}


