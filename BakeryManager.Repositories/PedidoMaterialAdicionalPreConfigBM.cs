using BakeryManager.Entities;
using BakeryManager.InfraEstrutura.Base.BusinessManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Repositories
{
    public class PedidoMaterialAdicionalPreConfigBM : BusinessManagementBase<PedidoMaterialAdicionalPreConfig>
    {
        public IList<PedidoMaterialAdicionalPreConfig> GetPreConfiguracaoByTipoPedido(TipoPedido tipoPedido)
        {
            return Query().Where(x => x.Evento.IdTipoPedido == tipoPedido.IdTipoPedido).ToList();
        }
    }
}
