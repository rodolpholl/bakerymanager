using BakeryManager.Entities;
using BakeryManager.Infraestrutura.Base.BusinessManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Repositories
{
    public class IngredienteBM : BusinessManagementBase<Ingrediente>
    {
        public Ingrediente GetByCodigoTACO(string Codigo)
        {
            return this.Query().FirstOrDefault(x => x.CodigoTACO.Equals(int.Parse(Codigo)));
        }
    }
}
