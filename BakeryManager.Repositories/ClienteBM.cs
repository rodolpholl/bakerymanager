using BakeryManager.Entities;
using BakeryManager.InfraEstrutura.Base.BusinessManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Repositories
{
    public class ClienteBM : BusinessManagementBase<Cliente>
    {
        public IList<Cliente> GetListaClienteByTipoCliente(int TipoCliente)
        {
            return Query().Where(x => x.TipoCliente == (TipoCliente)TipoCliente).ToList();
        }
    }
}
