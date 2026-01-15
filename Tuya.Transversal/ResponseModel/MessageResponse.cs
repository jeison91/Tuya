using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuya.Transversal.ResponseModel
{
    public record MessageResponse() : Response()
    {
        public int Status { get; set; }
        public string? Message { get; set; }
        public dynamic? Data { get; set; }
        public List<ErrorDtl>? Errors { get; set; }
    }

    public record ErrorDtl(string Code, string Message, string? Path = null);
}
