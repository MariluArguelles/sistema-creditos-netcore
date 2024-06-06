using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Dtos.Sales.Request
{
    public class SaleRequestDto
    {
        public int? CustomerId { get; set; }
        public double SubTotal { get; set; }
        public double Total { get; set; }
        public double Balance { get; set; }
        public bool Closed { get; set; }
        public bool Paid { get; set; }
        public int State { get; set; }

    }
}
