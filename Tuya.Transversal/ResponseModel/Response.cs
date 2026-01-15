using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuya.Transversal.ResponseModel
{
    public record Response() { }

    public record Response<T>(T? Data, string Message) : Response();
}
