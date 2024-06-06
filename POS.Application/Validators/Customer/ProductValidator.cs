using FluentValidation;
using POS.Application.Dtos.Product.Request;

namespace POS.Application.Validators.Customer
{
    public class ProductValidator : AbstractValidator<ProductRequestDto>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Description)
                .NotNull().WithMessage("El campo descripción no puede ser nulo")
                .NotEmpty().WithMessage("El campo descripción no puede ser vacío");

        }
    }
}
