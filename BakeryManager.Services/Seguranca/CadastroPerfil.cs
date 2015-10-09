using BakeryManager.Entities;
using BakeryManager.Infraestrutura.Base.BusinessProcess;
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
        public UsuarioPerfilBM usuarioPerfilBm;
        
        public CadastroPerfil()
        {
            perfilBm = GetObject<PerfilBM>();
            usuarioPerfilBm = GetObject<UsuarioPerfilBM>();
        }


        public IList<Perfil> GetListaPerfil()
        {
            //Colocando uma restrição para não retornar o perfil AdmMaster
            return perfilBm.Query().Where(x => x.IdPerfil != 1).ToList();
        }


        public void Dispose()
        {
            perfilBm.Dispose();
            usuarioPerfilBm.Dispose();
        }

        public void InserirPerfil(Perfil perfil)
        {
            perfilBm.Insert(perfil);
        }

        public Perfil GetPerfilById(int idPerfil)
        {
            return perfilBm.GetByID(idPerfil);
        }

        public void AlterarPerfil(Perfil perfil)
        {
            perfilBm.Update(perfil);
        }

        public void ExcluirPerfil(int idPerfil)
        {
            if (usuarioPerfilBm.Query().Any(x => x.Perfil.IdPerfil == idPerfil))
                throw new BusinessProcessException("'Não foi possível excluir o perfil selecionado! Exitem usuários associados a este perfil");
            else
                perfilBm.Delete(perfilBm.GetByID(idPerfil));
        }
    }
}
