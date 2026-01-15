using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuya.Domain.Entities
{
    public class CustomerEntity
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }
        public required string CellPhone { get; set; }
        public string Email { get; set; } = string.Empty;

        public ICollection<OrderEntity> Orders { get; set; } = [];
    }
}
