using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Dtos.Product.Request
{
    public class ProductRequestDto //datos que pide capturar 
    {
        public string Description { get; set; }
        public string Brand { get; set; }
        public double PurchaseCost { get; set; }
        public double SalesCost { get; set; }
        public int? CategoryId { get;set; } //entero??
        public int State { get; set; }
    }
}
