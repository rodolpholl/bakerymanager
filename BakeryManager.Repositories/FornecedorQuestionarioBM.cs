using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BakeryManager.Entities;
using BakeryManager.InfraEstrutura.Base.BusinessManagement;

namespace BakeryManager.Repositories
{
    public class FornecedorQuestionarioBM: BusinessManagementBase<FornecedorQuestionario>
    {
        public IList<FornecedorQuestionario> GetForneceodrQuestionarioByFornecedor(Fornecedor pForneceodor)
        {
            return Query().Where(x => x.Fornecedor.IdFornecedor == pForneceodor.IdFornecedor).ToList();
        }
            
    }
}
