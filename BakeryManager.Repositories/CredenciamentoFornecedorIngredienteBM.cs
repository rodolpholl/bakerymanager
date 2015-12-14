using BakeryManager.Entities;
using BakeryManager.InfraEstrutura.Base.BusinessManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Repositories
{
    public class CredenciamentoFornecedorIngredienteBM : BusinessManagementBase<CredenciamentoFornecedorIngrediente>
    {
        public IList<CredenciamentoFornecedorIngrediente> GetCredenciamentoByFornecedor(Fornecedor pFornecedor)
        {
            return Query().Where(x => x.Fornecedor.IdFornecedor == pFornecedor.IdFornecedor).ToList();
        }

        public IList<CredenciamentoFornecedorIngrediente> GetCredenciamentoByIngrediente(Ingrediente pIngrediente)
        {
            return Query().Where(x => x.Ingrediente.IdIngrediente == pIngrediente.IdIngrediente).ToList();
        }
    }
}
