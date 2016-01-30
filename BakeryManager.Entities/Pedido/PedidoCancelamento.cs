using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Entities
{

    public enum TipoCancelamenoPedido
    {
        AberturaIncoreta = 1,
        Desistencia = 2,
        PagamentoNaoEfetuado = 3
    }

    public class PedidoCancelamento
    {
        public virtual int IdPedidoCancelamento { get; set; }
        public virtual Pedido Pedido { get; set; } 
        public virtual string TextoCancelamento { get; set; }
        public virtual TipoCancelamenoPedido TipoCancelamento { get; set; }
        public virtual DateTime DataCancelamnto { get; set; }
        public virtual Usuario UsuarioCancelamento { get; set; }
        public virtual string IpCancelamento { get; set; }
    }
}
