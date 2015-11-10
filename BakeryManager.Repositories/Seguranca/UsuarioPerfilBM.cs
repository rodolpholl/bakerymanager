using BakeryManager.Entities;
using BakeryManager.InfraEstrutura.Base.BusinessManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Repositories.Seguranca
{
    public class UsuarioPerfilBM : BusinessManagementBase<UsuarioPerfil>
    {
        public IList<UsuarioPerfil> GetPerfilByUsuario(Usuario pUsuario)
        {
            return Query().Where(x => x.Usuario.IdUsuario == pUsuario.IdUsuario).ToList();
        }

        public IList<UsuarioPerfil> GetPerfilUsuarioByUsuario(Usuario pUsuario)
        {
            return Query().Any(x => x.Usuario.IdUsuario == pUsuario.IdUsuario) ? Query().Where(x => x.Usuario.IdUsuario == pUsuario.IdUsuario).ToList() : null;
        }
    }
}
