using BakeryManager.Entities;
using BakeryManager.InfraEstrutura.Base.BusinessManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Repositories
{
    public class PedidoProdutoBM : BusinessManagementBase<PedidoProduto>
    {
        public IList<PedidoProduto> GetPedidoProdutoByPedido(Pedido pedido)
        {
            return Query().Where(x => x.Pedido.IdPedido == pedido.IdPedido).ToList();
        }
    }
}
