using BakeryManager.Entities;
using BakeryManager.InfraEstrutura.Base.BusinessManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Repositories
{
    public class PedidoProdutoProduzidoBM : BusinessManagementBase<PedidoProdutoProduzido>
    {
        public IList<PedidoProdutoProduzido> GetProdutosByPedido(Pedido pedido)
        {
            return Query().Where(x => x.Pedido.IdPedido == pedido.IdPedido).ToList();
        }

        public PedidoProdutoProduzido GetByPedidoAndProduto(Pedido pedido, Produto produto)
        {
            return Query().FirstOrDefault(x => x.Pedido.IdPedido == pedido.IdPedido &&
                                               x.Produto.IdProduto == produto.IdProduto);
        }

        public IList<PedidoProdutoProduzido> GetProdutosProduzidosByPedidoList(IList<Pedido> listaPedidos)
        {

            var listaIdPedido = listaPedidos.Select(x => x.IdPedido).ToArray();
            return Query().Where(x => listaIdPedido.Contains(x.Pedido.IdPedido)).ToList();
        }
    }
}
