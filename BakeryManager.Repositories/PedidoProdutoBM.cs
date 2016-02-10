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

        public IList<PedidoProduto> GetPedidoProdutoByPedidoList(IList<Pedido> listaPedidos)
        {
            var listaIdPedidos = listaPedidos.Select(x => x.IdPedido).ToArray();
            return Query().Where(x => listaIdPedidos.Contains(x.Pedido.IdPedido)).ToList();

        }

        public IList<PedidoProduto> GetPedidoProdutoByProdutoAndStatusAtual(Produto produto, StatusPedido StatusPedido)
        {
            return Query().Where(x => x.Produto.IdProduto == produto.IdProduto && x.Pedido.StatusAtual == StatusPedido).ToList();
        }

        public PedidoProduto GetPedidoProdutoByPedidoEProduto(Pedido pedido, Produto produto)
        {
            return Query().FirstOrDefault(x => x.Pedido.IdPedido == pedido.IdPedido && x.Produto.IdProduto == produto.IdProduto);

        }
    }
}
