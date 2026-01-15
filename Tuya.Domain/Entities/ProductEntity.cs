using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuya.Domain.Entities
{
    public class ProductEntity
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = null!;
        public decimal UnitPrice { get; set; }
        public bool Active { get; set; }

        public ICollection<OrderDetailEntity> OrderDetails { get; set; } = [];
    }
}
