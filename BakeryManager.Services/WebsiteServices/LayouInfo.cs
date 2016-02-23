using BakeryManager.Entities;
using BakeryManager.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Services.WebsiteServices
{
    public class LayouInfo : BusinessProcessBase, IDisposable
    {
        private TipoPedidoBM tipoPedidoBM;
        private ProdutoBM produtoBm;
        
        public LayouInfo()
        {
            tipoPedidoBM = GetObject<TipoPedidoBM>();
            produtoBm = GetObject<ProdutoBM>();
        }
        public void Dispose()
        {
            tipoPedidoBM.Dispose();
            produtoBm.Dispose();
        }

        public IList<TipoPedido> GetListaTipoServico()
        {
            return tipoPedidoBM.GetListaAtivos();
        }

        public IList<Produto> GetListaNomeTipoProduto()
        {
            return produtoBm.GetProdutosAtivos();
        }
    }
}
