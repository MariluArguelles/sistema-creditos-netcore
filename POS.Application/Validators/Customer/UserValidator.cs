using FluentValidation;
using POS.Application.Dtos.Category.Request;
using POS.Application.Dtos.User.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Validators.Customer
{
    public class UserValidator : AbstractValidator<UserRequestDto>
    {
        public UserValidator()
        {
            RuleFor(x => x.UserName)
                .NotNull().WithMessage("El campo Nombre no puede ser nulo")
                .NotEmpty().WithMessage("El campo nombre no puede ser vacío");

            RuleFor(x => x.Email)
             .NotNull().WithMessage("El campo email no puede ser nulo")
             .NotEmpty().WithMessage("El campo email no puede ser vacío");

            RuleFor(x => x.Password)
             .NotNull().WithMessage("El campo contraseña no puede ser nulo")
             .NotEmpty().WithMessage("El campo contraseña no puede ser vacío");

        }
    }
}
