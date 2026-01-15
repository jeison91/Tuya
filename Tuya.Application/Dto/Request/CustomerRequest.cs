using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuya.Application.Dto.Request
{
    public class CustomerRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; } = string.Empty;
    }

    public class CustomerValidator : AbstractValidator<CustomerRequest>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("El ID debe ser mayor que 0.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre no puede superar los 100 caracteres.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("La Dirección es obligatorio.")
                .MaximumLength(100).WithMessage("La Dirección no puede superar los 100 caracteres.");

            RuleFor(x => x.CellPhone)
                .NotEmpty().WithMessage("El teléfono es obligatorio.")
                .MaximumLength(10).WithMessage("El teléfono no puede superar los 10 caracteres.")
                .Matches(@"^3\d{9}$").WithMessage("Formato invalido");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El correo es obligatorio.")
                .MaximumLength(100).WithMessage("El correo no puede superar los 100 caracteres.")
                .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$").WithMessage("Formato invalido");
        }
    }
}
