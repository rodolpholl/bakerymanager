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

        public IList<Ingrediente> GetListaIngredientesByFiltro(string textoPesquisa)
        {

            if (string.IsNullOrWhiteSpace(textoPesquisa))
                return GetAll();

            IList<Ingrediente> result;
            int codigoTACO = 0;

            int.TryParse(textoPesquisa, out codigoTACO);

            if (codigoTACO > 0)
                if (Query().Any(x => x.CodigoTACO == codigoTACO))
                    result = Query().Where(x => x.CodigoTACO == codigoTACO).ToList();
                else
                    result = new List<Ingrediente>();
            else
                result = Query().Where(x => x.Abreviatura.ToUpper().Contains(textoPesquisa.ToUpper()) ||
                                                x.Nome.ToUpper().Contains(textoPesquisa.ToUpper()) ||
                                                x.NomeTACO.ToUpper().Contains(textoPesquisa.ToUpper())).ToList();
            return result;

        }
    }
}
