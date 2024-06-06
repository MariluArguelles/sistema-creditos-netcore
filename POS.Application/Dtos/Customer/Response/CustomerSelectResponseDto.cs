using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Dtos.Customer.Response
{
    public class CustomerSelectResponseDto  //combo
    {
        public int CustomerId { get; set; }
        public string? Name { get; set; }
    }
}
