using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Dtos.SaleItem.Response
{
    public class SaleItemResponsetDto
    {
        public int SaleItemId { get; set; }
        public string ProductDescription { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
