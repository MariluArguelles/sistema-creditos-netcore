namespace POS.Application.Dtos.Customer.Response
{
    public class CustomerResponseDto
    {//campos a mostrar por la tabla
        public int CustomerId { get; set; }
        public string? Name { get; set; }
        public string? LastName1 { get; set; }
        public string? LastName2 { get; set; }
        public string? BirthDate { get; set; } 
        public int? Gender { get; set; }
        public string GenderText { get; set; }
        public string? Email { get; set; }
        public string? AuditCreateDate { get; set; }
        public int State { get; set; }
        public string? StateCustomer { get; set; } //Activo /Inactivo
    }
}
