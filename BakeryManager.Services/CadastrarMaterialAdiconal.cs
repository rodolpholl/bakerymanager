using BakeryManager.Entities;
using BakeryManager.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Services
{
    public class CadastrarMaterialAdiconal : BusinessProcessBase, IDisposable
    {
        private MaterialAdicionalBM materialAdicionalBm;

        public CadastrarMaterialAdiconal()
        {
            materialAdicionalBm = GetObject<MaterialAdicionalBM>();
        }
        public void Dispose()
        {
            materialAdicionalBm.Dispose();
        }

        public IList<MaterialAdicional> GetAll()
        {
            return materialAdicionalBm.GetAll();
        }

        public void InserirMaterialAdicional(MaterialAdicional materialAdicional)
        {
            materialAdicionalBm.Insert(materialAdicional);
        }

        public MaterialAdicional GetMaterialAdicionalById(int idMaterialAdional)
        {
            return materialAdicionalBm.GetByID(idMaterialAdional);
        }

        public void AlterarMaterialAdicional(MaterialAdicional materialAdicional)
        {
            materialAdicionalBm.Update(materialAdicional);
        }

        public void DesativarMaterialAdicional(MaterialAdicional materialAdicional)
        {
            materialAdicional.Ativo = false;
            materialAdicionalBm.Update(materialAdicional);
        }

        public void ReativarMaterialAdicional(MaterialAdicional materialAdicional)
        {
            materialAdicional.Ativo = true;
            materialAdicionalBm.Update(materialAdicional);
        }
    }
}
