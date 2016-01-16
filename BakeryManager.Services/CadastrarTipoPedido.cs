using BakeryManager.Entities;
using BakeryManager.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Services
{
    public class CadastrarTipoPedido : BusinessProcessBase, IDisposable
    {
        private TipoPedidoBM tipoPedidoBm;

        public CadastrarTipoPedido()
        {
            tipoPedidoBm = GetObject<TipoPedidoBM>();
        }
        public void Dispose()
        {
            tipoPedidoBm.Dispose();
        }

        public IList<TipoPedido> GetAll()
        {
            return tipoPedidoBm.GetAll();
        }

        public void InserirTipoPedido(TipoPedido tipoPedido)
        {
            tipoPedidoBm.Insert(tipoPedido);
        }

        public TipoPedido GetTipoPedidoById(int idTipoPedido)
        {
            return tipoPedidoBm.GetByID(idTipoPedido);
        }
        public void AlterarTipoPedido(TipoPedido tipoPedido)
        {
            tipoPedidoBm.Update(tipoPedido);
        }

        public void DesativarTipoPedido(TipoPedido tipoPedido)
        {
            tipoPedido.Ativo = false;
            tipoPedidoBm.Update(tipoPedido);
        }

        public void ReativarTipoPedido(TipoPedido tipoPedido)
        {
            tipoPedido.Ativo = true;
            tipoPedidoBm.Update(tipoPedido);
        }
    }
}
