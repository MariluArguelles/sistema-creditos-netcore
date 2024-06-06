using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Dtos.Category.Request
{
    public class CategoryRequestDto //datos que pide capturar 
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int State { get; set; }
    }

}
