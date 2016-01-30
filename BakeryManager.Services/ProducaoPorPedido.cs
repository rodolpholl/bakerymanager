using BakeryManager.Entities;
using BakeryManager.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Services
{
    public class ProducaoPorPedido : BusinessProcessBase, IDisposable
    {
        private PedidoProdutoProduzidoBM pedidoProdutoProduzidoBm;
        private ClienteBM clienteBm;
        private ProdutoBM produtoBm;
        private PedidoBM  pedidoBm;

        public ProducaoPorPedido()
        {
            pedidoProdutoProduzidoBm = GetObject<PedidoProdutoProduzidoBM>();
            clienteBm = GetObject<ClienteBM>();
            produtoBm = GetObject<ProdutoBM>();
            pedidoBm  = GetObject<PedidoBM>();
        }

        public void Dispose()
        {
            pedidoProdutoProduzidoBm.Dispose();
            clienteBm.Dispose();
            produtoBm.Dispose();
            pedidoBm.Dispose();
        }

        public IList<Cliente> GetListaCliente()
        {
            return clienteBm.GetListaClientesAtivos();
        }

        public IList<Produto> GetListaProduto()
        {
            return produtoBm.GetProdutosAtivos();
        }

        public Pedido GetPedidoByNumero(string NumeroPedido)
        {
            return pedidoBm.getPedidoByNumero(NumeroPedido);
        }

        public IList<Pedido> GetPedidosByFiltro(DateTime DataEntrega, DateTime? HorarioEntrega, int? IdCliente, string NumeroPedido)
        {
            return pedidoBm.GetPedidosByFiltro(DataEntrega, HorarioEntrega, clienteBm.GetByID(IdCliente.HasValue ? IdCliente.Value : 0), NumeroPedido);
        }

    }
}
