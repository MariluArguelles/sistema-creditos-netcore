using FluentValidation;
using POS.Application.Dtos.Category.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Validators.Customer
{
    public class CategoryValidator : AbstractValidator<CategoryRequestDto>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("El campo Nombre no puede ser nulo")
                .NotEmpty().WithMessage("El campo nombre no puede ser vacío");

        }
    }
}

