using BakeryManager.Entities;
using BakeryManager.InfraEstrutura.Base.BusinessProcess;
using BakeryManager.InfraEstrutura.Helpers.Security;
using BakeryManager.Repositories.Seguranca;
using System;
using System.Linq;



namespace BakeryManager.Services.Seguranca
{
    internal enum TipoValidacaoPassword
    {
        NovoAcesso,
        AlteracaoSenha
    }
    public class ControleAcesso : BusinessProcessBase, IDisposable
    {


        private UsuarioBM usuarioBm;
        private PerfilBM perfilBm;
        private UsuarioPerfilBM usuarioPerfilBm;
        private UsuarioHistoricoSenhaBM usuarioHistoricoSenhaBm;
        private UsuarioPerfilHistoricoBM usuarioPerfilHistoricoBm;
        private RegistroAcessoBM registroAcessoBm;
        private SenhaDiaBM senhaDiaBm;


        public Usuario GetUsuarioByLogin(string name)
        {
            return usuarioBm.GetByLogin(name);
        }

        public Perfil GetPerfilAtivo(Usuario usuarioLogado)
        {
            return usuarioPerfilBm.GetPerfilByUsuario(usuarioLogado).FirstOrDefault(x => x.Ativo).Perfil;
        }

        public ControleAcesso()
        {
            usuarioBm = GetObject<UsuarioBM>();
            usuarioPerfilBm = GetObject<UsuarioPerfilBM>();
            perfilBm = GetObject<PerfilBM>();
            usuarioHistoricoSenhaBm = GetObject<UsuarioHistoricoSenhaBM>();
            usuarioPerfilHistoricoBm = GetObject<UsuarioPerfilHistoricoBM>();
            registroAcessoBm = GetObject<RegistroAcessoBM>();
            senhaDiaBm = GetObject<SenhaDiaBM>();
        }

        public bool ValidaPendenciaPassword(string Login)
        {
            var user = usuarioBm.GetByLogin(Login);
            return (!user.AutenticaSenhaDia && string.IsNullOrWhiteSpace(user.Password));
        }

        public RegistroAcesso RegistrarLogin(string Login, string Senha, string Ip)
        {
            if (string.IsNullOrEmpty(Login))
                throw new BusinessProcessException("Informe o número do contrato!");

            //if (string.IsNullOrEmpty(Senha))
            //    throw new BusinessProcessException("Informe sua senha!");

            var rac = new RegistroAcesso()
            {
                DataHoraAcesso = DateTime.Now,
                NovoAcesso = false,
                Ip = Ip
            };



            var user = usuarioBm.GetByLogin(Login);

            if (user != null)
            {
                try
                {
                    rac.Usuario = user;

                    if (user.AutenticaSenhaDia && ValidaSenhaDia(Senha))
                    {
                        rac.Sucesso = true;
                        user.DataUltimoLogin = DateTime.Now;
                        usuarioBm.Update(user);
                    }

                    else if (ValidaNovoAcesso(user, Senha))
                    {
                        rac.Sucesso = true;
                        rac.NovoAcesso = true;
                        user.DataUltimoLogin = DateTime.Now;
                        usuarioBm.Update(user);
                    }
                    else if (user.Password != Senha)
                    {
                        rac.Sucesso = false;
                        rac.DescritivoErro = "Senha Incorreta! Tente Novamente.";
                    }
                    else
                    {
                        rac.Sucesso = true;
                        user.DataUltimoLogin = DateTime.Now;
                        usuarioBm.Update(user);
                    }

                }
                catch (Exception ex)
                {
                    rac.Sucesso = false;
                    rac.DescritivoErro = ex.Message;
                }
            }
            else
            {
                rac.DescritivoErro = "Usuário não localizado com o Login informado!";
            }

            registroAcessoBm.Insert(rac);

            if (!rac.Sucesso)
                throw new BusinessProcessException(rac.DescritivoErro);

            return rac;
        }



        private bool ValidaNovoAcesso(Usuario user, string senha)
        {
            try
            {
                return (string.IsNullOrWhiteSpace(user.Password) && string.IsNullOrWhiteSpace(senha));
                   
            }
            catch (Exception ex)
            {
                throw new BusinessProcessException(ex.Message);
            }
        }

        private void ValidarPassowordInformado(Usuario user, TipoValidacaoPassword tipoValidacao, string newPassword, string confirmNewPassword, string currentPassword = "")
        {

            /*if (newPassword.Length < 6)
                throw new BusinessProcessException("O password deve conter o mínimo de 6 caracteres!");

            if (newPassword.Length < 15)
                throw new BusinessProcessException("O password não pode ultrapassar 15 caracteres!");

            if (string.IsNullOrWhiteSpace(newPassword))
                throw new BusinessProcessException("Novo password é obrigatório!");

            if (string.IsNullOrWhiteSpace(confirmNewPassword))
                throw new BusinessProcessException("A confirmação do novo password é obrigatória!");

            if (!ValidatePasswordFormat(newPassword))
                throw new BusinessProcessException("Password informado Inválido! O novo password deve ser composto com pelo menos uma letra maiúscula, uma outra minúscula e um número.");

            if (newPassword != confirmNewPassword)
                throw new BusinessProcessException("A confirmação deve ser idêntica ao novo password informado!");*/

            if (tipoValidacao == TipoValidacaoPassword.AlteracaoSenha)
            {
                if (user.Password != currentPassword)
                    throw new BusinessProcessException("Password atual informado é diferente do password atual do usuário!");

                if (currentPassword.ToLower() == newPassword.ToLower())
                    throw new BusinessProcessException("Password informado igual ao anterior! Informe um novo password.");
            }

            
                
            


        }


        public void NovoAcesso(Usuario user, string newPassword, string confirmNewPassword)
        {
            try
            {
                ValidarPassowordInformado(user, TipoValidacaoPassword.NovoAcesso, newPassword, confirmNewPassword);
                RegistarAcessoUsuario(user, newPassword);
            }
            catch (Exception ex)
            {
                throw new BusinessProcessException(ex.Message);
            }

        }


        public void AlterarSenha(Usuario user, string currentPassword, string newPassword, string confirmNewPassword)
        {
            try
            {

                ValidarPassowordInformado(user, TipoValidacaoPassword.AlteracaoSenha, newPassword, confirmNewPassword, currentPassword);
                RegistarAcessoUsuario(user, newPassword);

            }
            catch (Exception ex)
            {
                throw new BusinessProcessException(ex.Message);
            }
        }

        private void RegistarAcessoUsuario(Usuario user, string newPassword)
        {
            var userHistoricoSenha = new UsuarioHistoricoSenha()
            {
                DataInclusao = DateTime.Now,
                Login = user.Login,
                Usuario = user,
                Senha = newPassword
            };

            usuarioHistoricoSenhaBm.Insert(userHistoricoSenha);
            user.Password = newPassword;

            usuarioBm.Update(user);
        }

        private bool ValidatePasswordFormat(string newPassword)
        {
            return PasswordHelper.ValidatePasswordFormat(newPassword);
        }

        private bool ValidaSenhaDia(string SenhaInformada)
        {
            var mascaraSenhaDia = senhaDiaBm.GetMascaraSenhaDiaAtiva().MascaraSenhaDia;
            return PasswordHelper.ValidatePasswordOfDay(mascaraSenhaDia, SenhaInformada);
        }

        public void Dispose()
        {
            usuarioBm.Dispose();
            usuarioPerfilBm.Dispose();
            perfilBm.Dispose();
            usuarioHistoricoSenhaBm.Dispose();
            usuarioPerfilHistoricoBm.Dispose();
            registroAcessoBm.Dispose();
            senhaDiaBm.Dispose();
        }
    }
}
