using FluentValidation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuya.Application.Dto.Request
{
    public class OrderRequest
    {
        public int IdCustomer { get; set; }
        public string Address { get; set; }
        public decimal Total { get; set; }
        public Collection<OrderDetailRequest> OrderDetails { get; set; }
    }

    public class OrderValidator : AbstractValidator<OrderRequest>
    {
        public OrderValidator()
        {
            RuleFor(x => x.IdCustomer)
                .GreaterThan(0).WithMessage("Se debe ingresar un cliente.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("La dirección es obligatorio.")
                .MaximumLength(100).WithMessage("La dirección no puede superar los 100 caracteres.");

            RuleFor(x => x.Total)
                .GreaterThan(0).WithMessage("La cantidad debe se mayor a 0.");
        }
    }
}
