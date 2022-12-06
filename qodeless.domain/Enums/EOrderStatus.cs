using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qodeless.Domain.Enum
{
    public enum EOrderStatus
    {
        Unpaid = 0, //Pendente
        PaidOut, //Pago
        Expired, //Vencido
        Returned, //Devolvido
    }
}
