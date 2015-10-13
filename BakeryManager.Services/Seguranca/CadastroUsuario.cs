using BakeryManager.Entities;
using BakeryManager.Repositories.Seguranca;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Services.Seguranca
{
    public class CadastroUsuario : BusinessProcessBase, IDisposable
    {
        private UsuarioBM usuarioBm;
        private PerfilBM perfilBm;


        public CadastroUsuario()
        {
            usuarioBm = GetObject<UsuarioBM>();
            perfilBm = GetObject<PerfilBM>();
        }

        public IList<Perfil> GetListaPerfil()
        {
            return perfilBm.GetAll();
        }

        public void Dispose()
        {
            usuarioBm.Dispose();
            perfilBm.Dispose();
       }
    }
}
