using BakeryManager.Entities;
using BakeryManager.Repositories;
using BakeryManager.Repositories.Seguranca;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Services
{
    public class ManterFuncionarios : BusinessProcessBase, IDisposable
    {
        private FuncionarioBM funcionarioBm;
        private UsuarioBM usuarioBm;
        private UsuarioPerfilBM usuarioPerfilBm;
        private PerfilBM perfilBm;


        public ManterFuncionarios()
        {
            funcionarioBm = GetObject<FuncionarioBM>();
            usuarioBm = GetObject<UsuarioBM>();
            usuarioPerfilBm = GetObject<UsuarioPerfilBM>();
            perfilBm = GetObject<PerfilBM>();
        }
        public void Dispose()
        {
            funcionarioBm.Dispose();
            usuarioBm.Dispose();
            usuarioPerfilBm.Dispose();
            perfilBm.Dispose();
        }

        public IList<Funcionario> GetFuncionariosAll()
        {
            return funcionarioBm.GetAll();
        }

        public IList<Perfil> GetListaPerfil()
        {
            return perfilBm.GetAll().Where(x => x.Ativo && x.IdPerfil != 1).OrderBy(x => x.Nome).ToList();
        }

        public Usuario GetUsuarioPorFuncionario(Funcionario Funcionario)
        {
            return usuarioBm.GetByFuncionario(Funcionario);
        }

        public Perfil GetPerfilByUsuario(Usuario Usuario)
        {
            return usuarioPerfilBm.GetPerfilByUsuario(Usuario).FirstOrDefault().Perfil;
        }

        public Funcionario GetFuncionarioById(int IdFuncionario)
        {
            return funcionarioBm.GetByID(IdFuncionario);
        }

        public void InserirFuncionario(Funcionario pFuncionario)
        {
            funcionarioBm.Insert(pFuncionario);
        }

        public void AlterarFuncionario(Funcionario pFuncionario)
        {
            funcionarioBm.Update(pFuncionario);
        }

        public void AtualizarFuncionario(Funcionario Funcionario, string Login, bool UsaSenhaDia, int IdPefil)
        {
            var UsuarioFuncionario = usuarioBm.GetByFuncionario(Funcionario);

            if (UsuarioFuncionario == null)
            {
                UsuarioFuncionario = new Usuario()
                {
                    DataCriacao = DateTime.Now,
                    AutenticaSenhaDia = UsaSenhaDia,
                    Ativo = true,
                    Email = Funcionario.Email,
                    Telefone = string.IsNullOrWhiteSpace(Funcionario.TelefoneCelular) ? Funcionario.TelefoneFixo : Funcionario.TelefoneCelular,
                    Login = Login.ToUpper(),
                    Nome = Funcionario.Nome.ToUpper(),
                    FuncionarioAssociado = funcionarioBm.GetByID(Funcionario.IdFuncionario)
                };

                usuarioBm.Insert(UsuarioFuncionario);

                var usuarioPerfil = new UsuarioPerfil()
                {
                    Ativo = true,
                    DataAssociacao = DateTime.Now,
                    Perfil = perfilBm.GetByID(IdPefil),
                    Usuario = usuarioBm.GetByID(UsuarioFuncionario.IdUsuario)
                };

                usuarioPerfilBm.Insert(usuarioPerfil);

            }
            else
            {
                UsuarioFuncionario.Login = Login.ToUpper();
                UsuarioFuncionario.Nome = Funcionario.Nome.ToUpper();
                UsuarioFuncionario.Email = Funcionario.Email;
                UsuarioFuncionario.Telefone = string.IsNullOrWhiteSpace(Funcionario.TelefoneCelular) ? Funcionario.TelefoneFixo : Funcionario.TelefoneCelular;
                UsuarioFuncionario.AutenticaSenhaDia = UsaSenhaDia;

                usuarioBm.Update(UsuarioFuncionario);

                var usuarioPerfil = usuarioPerfilBm.GetPerfilByUsuario(UsuarioFuncionario).Where(x => x.Ativo).FirstOrDefault();

                if (usuarioPerfil != null)
                {
                    usuarioPerfil.Ativo = false;
                    usuarioPerfilBm.Update(usuarioPerfil);

                    usuarioPerfil = new UsuarioPerfil()
                    {
                        Ativo = true,
                        DataAssociacao = DateTime.Now,
                        Perfil = perfilBm.GetByID(IdPefil),
                        Usuario = usuarioBm.GetByID(UsuarioFuncionario.IdUsuario)
                    };

                    usuarioPerfilBm.Insert(usuarioPerfil);

                }
   
            }
        }

        public void DesativarUsuario(Funcionario funcionario)
        {
            var usuarioPerfil = usuarioPerfilBm.GetPerfilByUsuario(usuarioBm.GetByFuncionario(funcionario)).Where(x => x.Ativo).FirstOrDefault();
            if (usuarioPerfil != null) 
            {
                usuarioPerfil.Ativo = false;
                usuarioPerfilBm.Update(usuarioPerfil);
            }

            var usuarioFuncionario = usuarioBm.GetByFuncionario(funcionario);
            if (usuarioFuncionario != null)
            {
                usuarioFuncionario.Ativo = false;
                usuarioBm.Update(usuarioFuncionario);
            }

        }
    }
}
