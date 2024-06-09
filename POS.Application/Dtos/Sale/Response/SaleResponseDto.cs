using POS.Application.Dtos.Sale.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Dtos.Sales.Response
{
    public class SaleResponseDto
    {
        public int saleId { get; set; }
        public int CustomerId { get; set; }
        public string SubTotal { get; set; }
        public string Total { get; set; }
        public string Balance { get; set; }
        public bool Closed { get; set; }
        public bool Paid { get; set; }
        public string? AuditCreateDate { get; set; }
         
        public ICollection<SaleItemResponse> ?SaleItems { get; set; } //prueba
        public ICollection<PaymentResponse> ?Payments { get; set; }  //prueba
    }


 


}
