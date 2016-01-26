using BakeryManager.Entities;
using BakeryManager.InfraEstrutura.Base.BusinessManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Repositories
{
    public class PedidoBM : BusinessManagementBase<Pedido>
    {
        public IList<Pedido> GetListaPedidosEncaminhados()
        {
            return Query().Where(x => x.StatusAtual == StatusPedido.Encaminhado).ToList();
        }
    }
}
