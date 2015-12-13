using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BakeryManager.Entities;
using BakeryManager.InfraEstrutura.Base.BusinessManagement;

namespace BakeryManager.Repositories
{
    public class FornecedorAvaliacaoQuestionarioRespostaBM : BusinessManagementBase<FornecedorAvaliacaoQuestionarioResposta>
    {
        public IList<FornecedorAvaliacaoQuestionarioResposta> GetForneceodrQuestionarioByFornecedor(Fornecedor fornecedor)
        {
            return Query().Where(x => x.FornecedorQuestionario.Avaliacao.Fornecedor.IdFornecedor == fornecedor.IdFornecedor).ToList();
        }
    }
}
