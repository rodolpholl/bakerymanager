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
        public LayouInfo()
        {
            tipoPedidoBM = GetObject<TipoPedidoBM>();
        }
        public void Dispose()
        {
            tipoPedidoBM.Dispose();
        }

        public IList<TipoPedido> GetListaTipoServico()
        {
            return tipoPedidoBM.GetListaAtivos();
        }
    }
}
