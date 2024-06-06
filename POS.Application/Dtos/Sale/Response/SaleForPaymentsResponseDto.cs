using POS.Domain.Entities;

namespace POS.Application.Dtos.Sale.Response
{
    public class SaleForPaymentsResponseDto
    {
        public int SaleId { get; set; }
        public string Total { get; set; }
        public string SubTotal { get; set; }
        public string Balance { get; set; }
        public bool Closed { get; set; }
        public bool Paid { get; set; }
        public string? AuditCreateDate { get; set; }
        public ICollection<SaleItemResponse> SaleItems { get; set; }
        public ICollection<PaymentResponse> Payments { get; set; }
        public int? AuditDeleteUser { get; set; }
        public DateTime? AuditDeleteDate { get; set; }
        public int CustomerId { get; set; }
    }

    public class SaleItemResponse
    {
        public int Id { get; set; }
        public decimal Quantity { get; set; }
    //    public ProductResponse Product { get; set; } // Puedes ajustar el tipo según tu modelo
        public decimal Price { get; set; }
        public string Description { get; set; }
    }

    public class PaymentResponse
    {
        public decimal Quantity { get; set; }
        public decimal Balance { get; set; }
        public DateTime PaymentDate { get; set; }
    }

    public  class ProductResponse 
    {
        public string Description { get; set; } = null!;

    }

    //public partial class Product : BaseEntity
    //{
    //    public string Description { get; set; } = null!;

    //}
}
