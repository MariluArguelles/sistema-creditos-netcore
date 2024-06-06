using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Dtos.Payment.Response
{
    public class PaymentResponseDto
    {
        public int PaymentId { get; set; }
        
        public decimal Quantity { get; set; }

        public decimal Balance { get; set; }

        public string PaymentDate { get; set; }//la fecha que agrega el usuario

    }
}
