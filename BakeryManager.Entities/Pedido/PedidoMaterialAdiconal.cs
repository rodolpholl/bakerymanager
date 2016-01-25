using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Entities
{
    public class PedidoMaterialAdiconal
    {
        public virtual  int IdPedidoMaterialAdiconal { get; set; }
        public virtual Pedido Pedido { get; set; }
        public virtual MaterialAdicional Material { get; set; }
        public virtual double Quantidade { get; set; }
        public virtual TipoAquisicaoTemporaria TipoAquisicao { get; set; }
        public virtual double PrecoUnitario { get; set; }
        public virtual double PrecoTotal { get; set; }
        
    }
}
