using BakeryManager.Entities;
using BakeryManager.InfraEstrutura.Base.BusinessManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Repositories
{
    public class MaterialAdicionalBM : BusinessManagementBase<MaterialAdicional>
    {
        public IList<MaterialAdicional> GetMaterialForaLista(int[] listaMaterialSelect)
        {
            return Query().Where(x => !listaMaterialSelect.Contains(x.IdMaterialAdicional)).ToList();
        }
    }
}
