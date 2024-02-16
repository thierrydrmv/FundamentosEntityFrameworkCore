using EFCore.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.Domain
{
    public class Pedido
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set;}
        public DateTime IniciadoEm { get; set; }
        public DateTime FinalizadoEm { get; set; }
        public ETipoFrete TipoFrete { get; set; }
        public EStatusPedido StatusPedido { get; set; }
        public string Observacao { get; set; }
        public ICollection<PedidoItem> Itens { get; set; }
    }
}
