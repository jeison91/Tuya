using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuya.Domain.Entities
{
    public class OrderDetailEntity
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }

        public OrderEntity OrderEntity { get; set; } = null!;
        public ProductEntity ProductEntity { get; set; } = null!;
    }
}
