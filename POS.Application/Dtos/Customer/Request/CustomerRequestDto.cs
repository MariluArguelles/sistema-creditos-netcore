using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Dtos.Customer.Request
{
    //Atributos para Registrar
    public class CustomerRequestDto
    {
        public string? Name { get; set; }
        public string? LastName1 { get; set; }
        public string? LastName2 { get; set; }
        public string? BirthDate { get; set; }
        public int? Gender { get; set; }
        public string? Email { get; set; }

        public int State { get; set; }
    }
}
