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

        public IList<Pedido> GetListaPedidosAguardandoProducao()
        {
            return Query().Where(x => x.StatusAtual == StatusPedido.AguardandoInicioProducao).ToList();
        }

        public IList<Pedido> GetListaPedidosEmProducao()
        {
            return Query().Where(x => x.StatusAtual == StatusPedido.EmProducao).ToList();
        }

        public IList<Pedido> GetListaPedidosEmEntrega()
        {
            return Query().Where(x => x.StatusAtual == StatusPedido.AguardandoEntrega).ToList();
        }

        public Pedido getPedidoByNumero(string NumeroPedido)
        {
            return Query().FirstOrDefault(x => x.NumeroPedido == NumeroPedido);
        }

        public IList<Pedido> GetPedidosByFiltro(DateTime? DataEntrega, DateTime? HoraEntrega, Cliente Cliente, string NUmeroPedido)
        {
            
            var query = Query();

            if (DataEntrega.HasValue)
                query = query.Where(x => x.DataEvento.Date >= DataEntrega.Value.Date);


            if (HoraEntrega.HasValue)
                query = query.Where(x => (x.DataHoraEntrega.Hour == HoraEntrega.Value.Hour &&
                                         x.DataHoraEntrega.Minute == HoraEntrega.Value.Minute) &&
                                         (x.DataHoraEntrega.Hour <= 23 && x.DataHoraEntrega.Minute <= 59));


            if (Cliente != null)
                query = query.Where(x => x.Cliente.IdCliente == Cliente.IdCliente);


            if (!string.IsNullOrWhiteSpace(NUmeroPedido))
                query = query.Where(x => x.NumeroPedido == NUmeroPedido);
          

            return query.ToList();

        }

        
    }
}
