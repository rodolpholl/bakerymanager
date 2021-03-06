﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BakeryManager.Services.Seguranca;
using BakeryManager.BackOffice.Controllers;
using System.Web;
using System.Web.Security;
using BakeryManager.BackOffice.Models;
using BakeryManager.Entities;
using BakeryManager.InfraEstrutura.Helpers;
using System.Web.Mvc;

namespace BakeryManager.BackOffice.Helpers
{
    public class WebHelpers
    {
        internal static dynamic ObterPerfilUsuario(string Login)
        {
            using (var controleAcesso = new ControleAcesso())
            {

                var UsuarioLogado = controleAcesso.GetUsuarioByLogin(Login);
                var PerfilUsuario = controleAcesso.GetPerfilAtivo(UsuarioLogado);
                
                return new
                {
                    Perfil = PerfilUsuario.Nome,
                    TipoUsuario = PerfilUsuario.Atribuicao.Equals(Rule.Administrador) ? TipoUsuarioEnum.Admin :
                                  PerfilUsuario.Atribuicao.Equals(Rule.Operador) ? TipoUsuarioEnum.Operador : TipoUsuarioEnum.Cliente
                };
                

            }
        }
        

        internal static void LogOut()
        {
            
            FormsAuthentication.SignOut();
            
            if (HttpContext.Current != null)
            {
                if (HttpContext.Current.Session != null)
                {
                    HttpContext.Current.Session.Contents.Clear();
                    HttpContext.Current.Session.Contents.RemoveAll();
                    HttpContext.Current.Session.Abandon();
                }

                HttpContext.Current.Response.Cookies.Clear();
                
                HttpContext.Current.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, "")
                {
                    Expires = DateTime.Now.AddYears(-1)
                });

                HttpContext.Current.Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", "")
                {
                    Expires = DateTime.Now.AddYears(-1)
                });
            }

            

        }

        internal static bool ValidaSenhaVazia(string Login)
        {
            using (var controleAcesso = new ControleAcesso())
            {
                return controleAcesso.ValidaPendenciaPassword(Login);
            }
        }

        internal static string ObterNovoUID()
        {
            return ProcessHelper.GetRandomUID();
        }

        internal static string GetErrorsAsString(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.Where(x => x.Errors.Count() > 0).Select(x => x.Errors.Select(y => y.ErrorMessage).Distinct().ToList()).ToList();
            
            var strErro = "<p>Erro na Atualização:</p><p>";

            foreach (var erro in erros.SelectMany(x => x).ToList())
                strErro += string.Concat(erro, "<br/>");

            strErro += "</p>";

            return strErro;
        }
    }
}
