using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Dtos.Category.Response
{
    public class CategoryResponseDto  
    {
        public int CategoryId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string AuditCreateDate { get; set; }
        public int State { get; set; }
        public string? StateCategory { get; set; }
    }
}
