using BakeryManager.BackOffice.Helpers;
using BakeryManager.BackOffice.Models.Login;
using BakeryManager.Entities;
using BakeryManager.InfraEstrutura.Helpers;
using BakeryManager.Services.Seguranca;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BakeryManager.BackOffice.Controllers
{
    public class SharedController : System.Web.Mvc.Controller
    {
        // GET: Shared
        public JsonResult GetLoginInformation()
        {
            using (var controleAcesso = new ControleAcesso())
            {
                var user = controleAcesso.GetUsuarioByLogin(User.Identity.Name);
                
                if (controleAcesso.ValidaPendenciaPassword(user.Login))
                {
                    WebHelpers.LogOut();
                    Json(new LoginModel()
                    {
                        Login = "Error"
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {

                }
                return Json(new LoginModel()
                {
                    Login = user.Login,
                    Nome = user.Nome,
                    Atribuicao = (int)controleAcesso.GetPerfilAtivo(user).Atribuicao
                }, JsonRequestBehavior.AllowGet);

            }
        }

       

        [Authorize]
        public JsonResult GetMenuLateral(byte Atribuicao)
        {
            
            var attr = (Rule)Enum.Parse(typeof(Rule), Atribuicao.ToString());
            string retorno = string.Empty;

            switch (attr)
            {
                case Rule.Administrador:
                    retorno = MVCHelper.RenderRazorViewToString(this, Url.Content("~/Views/Shared/_MenuAdministrador.cshtml"),null);
                    break;
                case Rule.Operador:
                    retorno = MVCHelper.RenderRazorViewToString(this, Url.Content("~/Views/Shared/_MenuOperador.cshtml"), null);
                    break;
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

    }
}