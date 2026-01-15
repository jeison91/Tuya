using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuya.Application.Enums
{
    public enum StateOrder
    {
        [Description("En Proceso")]
        PROCESS = 1,
        [Description("Finalizado")]
        FINISH = 2,
        [Description("Cancelado")]
        CANCEL =3
    }
}
