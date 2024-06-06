using FluentValidation;
using POS.Application.Dtos.Category.Request;
using POS.Application.Dtos.Payment.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Validators.Customer
{
    public class PaymentValidator : AbstractValidator<PaymentRequestDto>
    {
        public PaymentValidator()
        {
            RuleFor(x => x.Quantity)
                .NotNull().WithMessage("El campo Nombre no puede ser nulo")
                .NotEmpty().WithMessage("El campo nombre no puede ser vacío");
            RuleFor(x => x.PaymentDate)
               .NotNull().WithMessage("El campo fecha no puede ser nulo")
               .NotEmpty().WithMessage("El campo fecha no puede ser vacío");

        }
    }

}
