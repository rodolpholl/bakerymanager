using BakeryManager.Entities;
using BakeryManager.InfraEstrutura.Base.BusinessManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Repositories
{
    public class FornecedorQuestionarioConfigBM : BusinessManagementBase<FornecedorQuestionarioConfig>
    {
        public IList<FornecedorQuestionarioConfig> GetForneceodrQuestionarioByFornecedor(Fornecedor pForneceodor)
        {
            return Query().Where(x => x.Fornecedor.IdFornecedor == pForneceodor.IdFornecedor).ToList();
        }
    }
}
