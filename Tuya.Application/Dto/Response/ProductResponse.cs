using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuya.Application.Dto.Response
{
    public class ProductResponse
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = null!;
        public decimal UnitPrice { get; set; }
        public bool Active { get; set; }
    }
}
