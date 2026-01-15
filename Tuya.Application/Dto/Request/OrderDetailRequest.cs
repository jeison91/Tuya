using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuya.Application.Dto.Request
{
    public class OrderDetailRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class OrderDetailValidator : AbstractValidator<OrderDetailRequest>
    {
        public OrderDetailValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0).WithMessage("Se debe ingresar un producto.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("La cantidad debe se mayor a 0.");

            RuleFor(x => x.UnitPrice)
                .GreaterThan(0).WithMessage("El precio debe se mayor a 0.");

            RuleFor(x => x.TotalPrice)
                .GreaterThan(0).WithMessage("El Total debe se mayor a 0.");
        }
    }
}
