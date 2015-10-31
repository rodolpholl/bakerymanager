using BakeryManager.Entities;
using BakeryManager.Infraestrutura.Base.BusinessManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Repositories
{
    public class IngredienteTabelaNutricionalBM : BusinessManagementBase<IngredienteTabelaNutricional>
    {
        public IList<IngredienteTabelaNutricional> GetInformacoesNutricionaisByIngrediente(Ingrediente ingrediente)
        {
            return Query().Where(x => x.Ingrediente.IdIngrediente == ingrediente.IdIngrediente).ToList();
        }

        public bool ValidateByIngredienteeComponente(int idIngrediente, int idTabelaNutricional)
        {
            return Query().Any(x => x.Ingrediente.IdIngrediente == idIngrediente && x.Componente.IdTabelaNutricional == idTabelaNutricional);
        }

        public IList<IngredienteTabelaNutricional> GetComponentesByListaIdIngrediente(List<int> listIdIngrediente)
        {
            var result =  Query().Where(x => listIdIngrediente.Contains(x.Ingrediente.IdIngrediente)).Distinct().ToList();
            return result;
        }
    }
}
