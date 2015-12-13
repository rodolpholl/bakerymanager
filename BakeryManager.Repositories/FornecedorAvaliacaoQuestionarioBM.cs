using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BakeryManager.Entities;
using BakeryManager.InfraEstrutura.Base.BusinessManagement;

namespace BakeryManager.Repositories
{
    public class FornecedorAvaliacaoQuestionarioBM: BusinessManagementBase<FornecedorAvaliacaoQuestionario>
    {
        public IList<FornecedorAvaliacaoQuestionario> GetForneceodrQuestionarioByFornecedor(Fornecedor pForneceodor)
        {
            return Query().Where(x => x.Avaliacao.Fornecedor.IdFornecedor == pForneceodor.IdFornecedor).ToList();
        }

        public IList<FornecedorAvaliacaoQuestionario> getFornecedorQuestionarioByAvaliacao(FornecedorAvaliacao fornecedorAvaliacao)
        {
            return Query().Where(x => x.Avaliacao.IdFornecedorAvaliacao == fornecedorAvaliacao.IdFornecedorAvaliacao).ToList();
        }
    }
}
