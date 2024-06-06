using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Dtos.Product.Response
{
    public class ProductResponseDto  //campos a mostrar por la tabla
    {
        public int ProductId { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public string PurchaseCost { get; set; }
        public string SalesCost { get; set; }

        public int categoryId { get; set; }
        public string? AuditCreateDate { get; set; }
        public int State { get; set; }
        public string? StateProduct { get; set; } //Activo /Inactivo //kitar
    }
}
