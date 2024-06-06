using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Dtos.Payment.Request
{
    public class PaymentRequestDto
    {
        public int SaleId { get; set; }

        public decimal Quantity { get; set; }

        public decimal Balance { get; set; }

        public DateTime PaymentDate { get; set; } //la fecha que agrega el usuario

        public string? Description { get; set; } = null!;

        public int State { get; set; }
    }
}
