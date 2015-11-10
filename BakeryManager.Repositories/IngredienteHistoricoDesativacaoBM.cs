using BakeryManager.Entities;
using BakeryManager.InfraEstrutura.Base.BusinessManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Repositories
{
    public class IngredienteHistoricoDesativacaoBM : BusinessManagementBase<IngredienteHistoricoDesativacao>
    {
        public IList<IngredienteHistoricoDesativacao> GetHistoricoDesativacaoByIngrediente(Ingrediente pIngrediente)
        {
            return Query().Where(x => x.Ingrediente.IdIngrediente == pIngrediente.IdIngrediente).ToList();
        }
    }
}
