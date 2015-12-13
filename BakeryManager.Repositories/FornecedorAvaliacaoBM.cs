using BakeryManager.Entities;
using BakeryManager.InfraEstrutura.Base.BusinessManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Repositories
{
    public class FornecedorAvaliacaoBM : BusinessManagementBase<FornecedorAvaliacao>
    {
        public IList<FornecedorAvaliacao> GetListaAvaliacaoByFornecedor(Fornecedor fornecedor)
        {
            return Query().Where(x => x.Fornecedor.IdFornecedor == fornecedor.IdFornecedor).ToList();
        }
    }
}
