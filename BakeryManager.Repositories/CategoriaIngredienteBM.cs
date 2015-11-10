using BakeryManager.Entities;
using BakeryManager.InfraEstrutura.Base.BusinessManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Repositories
{
    public class CategoriaIngredienteBM : BusinessManagementBase<CategoriaIngrediente>
    {
        public CategoriaIngrediente GetByNome(string NomeCategoria)
        {
            return Query().FirstOrDefault(x => x.Nome.ToUpper() == NomeCategoria.ToUpper());
        }
    }
}
