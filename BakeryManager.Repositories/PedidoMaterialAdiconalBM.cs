using BakeryManager.Entities;
using BakeryManager.InfraEstrutura.Base.BusinessManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Repositories
{
    public class PedidoMaterialAdiconalBM : BusinessManagementBase<PedidoMaterialAdiconal>
    {
        public IList<PedidoMaterialAdiconal> GetMaterialAdicionalByPedido(Pedido pedido)
        {
            return Query().Where(x => x.Pedido.IdPedido == pedido.IdPedido).ToList();
        }

        
    }
}
