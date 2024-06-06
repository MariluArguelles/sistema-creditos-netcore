using FluentValidation;
using POS.Application.Dtos.Customer.Request;

namespace POS.Application.Validators.Customer
{
    public class CustomerValidator : AbstractValidator<CustomerRequestDto>
    {
        public CustomerValidator() 
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("El campo nombre no puede ser nulo")
                .NotEmpty().WithMessage("El campo nombre no puede ser vacío");
        }
    }
}
