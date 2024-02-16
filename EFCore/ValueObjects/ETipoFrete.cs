using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.ValueObjects
{
    public enum ETipoFrete
    {
        // remetente paga o frete
        CIF,
        // destinatário paga o frete
        FOB,
        SemFrete,
    }
}
