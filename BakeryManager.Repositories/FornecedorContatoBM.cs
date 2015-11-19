using BakeryManager.Entities;
using BakeryManager.InfraEstrutura.Base.BusinessManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Repositories
{
    public class FornecedorContatoBM : BusinessManagementBase<FornecedorContato>
    {
        public IList<FornecedorContato> GetByFornecedor(Fornecedor fornecedor)
        {
            return Query().Where(x => x.Fornecedor.IdFornecedor == fornecedor.IdFornecedor).ToList();
        }
    }
}
