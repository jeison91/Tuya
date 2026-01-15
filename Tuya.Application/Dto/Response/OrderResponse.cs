using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tuya.Domain.Entities;

namespace Tuya.Application.Dto.Response
{
    public class OrderResponse
    {
        public int Id { get; set; }
        public int IdCustomer { get; set; }
        public int CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public required string Address { get; set; }
        public required string State { get; set; }
        public decimal Total { get; set; }
        public ICollection<OrderDetailResponse> OrderDetails { get; set; } = [];
    }
}
