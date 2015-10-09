using BakeryManager.Entities;
using BakeryManager.Infraestrutura.Base.BusinessManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Repositories
{
    public class TabelaNutricionalBM : BusinessManagementBase<TabelaNutricional>
    {
        public TabelaNutricional GetByIngrediente(int IdIngrediente)
        {
            return Query().FirstOrDefault(x => x.Ingrediente.IdIngrediente.Equals(IdIngrediente));
        }
    }
}
