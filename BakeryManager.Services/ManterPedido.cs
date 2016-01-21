using BakeryManager.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Services
{
    public class ManterPedido : BusinessProcessBase, IDisposable
    {

        private PedidoBM pedidoBm;
        private PedidoHistoricoStatusBM pedidoHistoricoStatusBm;
        private PedidoMaterialAdiconalBM pedidoMaterialAdiconalBm;
        private PedidoProdutoBM pedidoProdutoBm;
        private ProdutoBM produtoBm;
        private PedidoMaterialAdicionalPreConfigBM PreConfigPedidoMaterialAdicionalBm;
        public ManterPedido()
        {
            pedidoHistoricoStatusBm = GetObject<PedidoHistoricoStatusBM>();
            pedidoBm = GetObject<PedidoBM>();
            pedidoMaterialAdiconalBm = GetObject<PedidoMaterialAdiconalBM>();
            pedidoProdutoBm = GetObject<PedidoProdutoBM>();
            produtoBm = GetObject<ProdutoBM>();
            PreConfigPedidoMaterialAdicionalBm = GetObject<PedidoMaterialAdicionalPreConfigBM>();
        }
        public void Dispose()
        {
            pedidoBm.Dispose();
            pedidoHistoricoStatusBm.Dispose();
            pedidoMaterialAdiconalBm.Dispose();
            pedidoProdutoBm.Dispose();
            produtoBm.Dispose();
            PreConfigPedidoMaterialAdicionalBm.Dispose();
        }
    }
}
