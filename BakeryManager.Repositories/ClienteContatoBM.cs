using BakeryManager.Entities;
using BakeryManager.InfraEstrutura.Base.BusinessManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Repositories
{
    public class ClienteContatoBM : BusinessManagementBase<ClienteContato>
    {
        public IList<ClienteContato> GetByCliente(Cliente cliente)
        {
            return Query().Where(x => x.Cliente.IdCliente == cliente.IdCliente).ToList();
        }
    }
}
