using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Entities
{
    public enum TipoAquisicaoTemporaria
    {
        Aluguel = 1,
        Compra = 2
    }
    public class PedidoMaterialAdicionalPreConfig
    {
        public virtual int IdPedidoMaterialAdicionalPreConfig { get; set; }
        public virtual TipoPedido Evento { get; set; }
        public virtual double Quantidade { get; set; }
        public virtual TipoAquisicaoTemporaria TipoAquisicao { get; set; }
        public virtual MaterialAdicional Material { get; set; }
    }
}
