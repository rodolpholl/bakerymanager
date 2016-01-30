using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Entities
{
    public class PedidoProdutoProduzido
    {
        public virtual int IdPedidoProdutoProduzido { get; set; }
        public virtual Pedido Pedido { get; set; }
        public virtual Produto Produto { get; set; }
        public virtual int Quantidade { get; set; }
        public virtual DateTime DataHoraInicioFabricacao { get; set; }
        public virtual DateTime DataHoraFimFabricacao { get; set; }
        public virtual int TempoProducao { get; set; }
    }
}
