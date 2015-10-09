using BakeryManager.Entities;
using BakeryManager.Infraestrutura.Base.BusinessManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Repositories.Seguranca
{
    public class UsuarioPerfilBM : BusinessManagementBase<UsuarioPerfil>
    {
        public IList<UsuarioPerfil> GetPerfilByUsuario(Usuario usuarioLogado)
        {
            return Query().Where(x => x.Usuario.IdUsuario == usuarioLogado.IdUsuario).ToList();
        }
    }
}
