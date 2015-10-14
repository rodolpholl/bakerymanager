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
        private UsuarioPerfilBM usuarioPerfilBM;


        public CadastroUsuario()
        {
            usuarioBm = GetObject<UsuarioBM>();
            perfilBm = GetObject<PerfilBM>();
            usuarioPerfilBM = GetObject<UsuarioPerfilBM>();
        }

        public void Dispose()
        {
            usuarioBm.Dispose();
            perfilBm.Dispose();
            usuarioPerfilBM.Dispose();
        }

        public IList<Perfil> GetListaPerfilAtivo()
        {
            return perfilBm.Query().Where(x => x.Ativo && x.IdPerfil != 1).ToList();
        }



        public IList<Usuario> GetListaUsuario()
        {
            return usuarioBm.GetAll().Where(x => x.Login != "BM_MASTER").ToList();
        }

        public Perfil GetPerfilAtivoByUsuario(Usuario pUsuario)
        {
            return usuarioPerfilBM.GetPerfilByUsuario(pUsuario).FirstOrDefault(x => x.Ativo).Perfil;
        }

        public IList<UsuarioPerfil> GetPerfilUsuarioByUsuario(Usuario pUsuario)
        {
            return usuarioPerfilBM.GetPerfilUsuarioByUsuario(pUsuario);
        }

        public void InserirUsuario(Usuario usuario)
        {
            usuarioBm.Insert(usuario);
        }

        public Usuario GetUsuarioById(int idUsuario)
        {
            return usuarioBm.GetByID(idUsuario);
        }

        public void AlterarUsuario(Usuario usuario)
        {
            usuarioBm.Update(usuario);
        }

        public void AtualizarAssociacaoPerfil(Usuario usuario, Perfil perfil)
        {
            
            var ListaPerfil = usuarioPerfilBM.GetPerfilUsuarioByUsuario(usuario);

            if (ListaPerfil == null)
            {
                var NovoPerfilAtivo = new UsuarioPerfil()
                {
                    Ativo = true,
                    DataAssociacao = DateTime.Now,
                    Perfil = perfil,
                    Usuario = usuario
                };

                usuarioPerfilBM.Insert(NovoPerfilAtivo);
            }
            else
            {
                var ultimoPerfilAtivo = ListaPerfil.FirstOrDefault(x => x.Ativo);
                if (ultimoPerfilAtivo.Perfil.IdPerfil != perfil.IdPerfil)
                {
                    ultimoPerfilAtivo.Ativo = false;
                    usuarioPerfilBM.Update(ultimoPerfilAtivo);

                    var NovoPerfilAtivo = new UsuarioPerfil()
                    {
                        Ativo = true,
                        DataAssociacao = DateTime.Now,
                        Perfil = perfil,
                        Usuario = usuario
                    };

                    usuarioPerfilBM.Insert(NovoPerfilAtivo);
                }
            }
            
          

        }

        public void DesativarUsuario(Usuario usuario)
        {
            usuario.Ativo = false;
            usuarioBm.Update(usuario);
        }

        public void ReativarUsuario(Usuario usuario)
        {
            usuario.Ativo = true;
            usuarioBm.Update(usuario);
        }

        public Perfil GetPerfilById(int idPerfil)
        {
            return perfilBm.GetByID(idPerfil);
        }
    }
}
