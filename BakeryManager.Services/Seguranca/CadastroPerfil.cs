using BakeryManager.Entities;
using BakeryManager.Repositories.Seguranca;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Services.Seguranca
{
    public class CadastroPerfil: BusinessProcessBase, IDisposable
    {
        private PerfilBM perfilBm;

        public CadastroPerfil()
        {
            perfilBm = GetObject<PerfilBM>();
        }


        public IList<Perfil> GetListaPerfil()
        {
            return perfilBm.GetAll();
        }


        public void Dispose()
        {
            perfilBm.Dispose();
        }
    }
}
