using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Entities
{
    public class PedidoRegistroEntrega
    {
        public virtual int IdPedidoRegistroEntrega { get; set; }
        public virtual Pedido Pedido { get; set; }
        public virtual EventoEntrega Evento { get; set; }
        public virtual string Justificativa { get; set; }
        public virtual DateTime DataRegistro { get; set; }
    }

    public enum EventoEntrega
    {
        Sucesso = 1,
        ErroNoPedido = 2,
        EnderecoNaoEncontrado = 3,
        EnderecoErrado = 4
    }
}
