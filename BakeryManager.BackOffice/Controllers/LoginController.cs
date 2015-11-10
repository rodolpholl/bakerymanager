using BakeryManager.BackOffice.Helpers;
using BakeryManager.BackOffice.Controllers;
using BakeryManager.BackOffice.Models.Login;
using BakeryManager.InfraEstrutura.Base.BusinessProcess;
using BakeryManager.Services.Seguranca;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BakeryManager.BackOffice.Models.Login;
using BakeryManager.BackOffice.Models;

namespace BakeryManager.BackOffice.Controllers
{

    public class LoginController : System.Web.Mvc.Controller
    {

        // GET: Login
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View(new LoginModel());
        }

        [HttpPost]
        public JsonResult Index(LoginModel Login)
        {
            if (ModelState.IsValid)
            {
                using (var controleAcesso = new ControleAcesso())
                {
                    try
                    {
                        var ra = controleAcesso.RegistrarLogin(Login.Login, Login.Senha, Request.ServerVariables["REMOTE_ADDR"]);


                        FormsAuthentication.SetAuthCookie(Login.Login, false);


                        //if (ra.NovoAcesso)
                        //    return RedirectToAction("NovoAcesso");
                        //else

                        //    return RedirectToAction("Index", "Home");

                        return Json(
                               new
                               {
                                   TipoMensagem = TipoMensagemRetorno.Ok,
                                   Mensagem = ra.NovoAcesso ? "NovoAcesso" : string.Empty,
                                   URLDestino = Url.Action("Index", "Home")

                               }, "text/html", JsonRequestBehavior.AllowGet);
                    }
                    catch (BusinessProcessException ex)
                    {
                        return Json(
                               new
                               {
                                   TipoMensagem = TipoMensagemRetorno.Erro,
                                   Mensagem = ex.Message
                                   
                               }, "text/html", JsonRequestBehavior.AllowGet);
                    }
                }
            }
            else
            {
                return Json(
                               new
                               {
                                   TipoMensagem = TipoMensagemRetorno.Erro,
                                   Mensagem = "Formulário Inválido! Verifique os dados enviados.",
                                   URLDestino = Url.Action("Index", "Home")

                               }, "text/html", JsonRequestBehavior.AllowGet);
            }


        }

        [Authorize]
        public ActionResult NovoAcesso()
        {
            return View();
        }


        [Authorize]
        [HttpPost]
        public JsonResult RegistrarNovoAcesso(NovoAcessoModel NovoAcesso)
        {
            try
            {
                using (var controleAcesso = new ControleAcesso())
                {

                    var user = controleAcesso.GetUsuarioByLogin(User.Identity.Name);

                    controleAcesso.NovoAcesso(user, NovoAcesso.NewPassword, NovoAcesso.ConfirmNewPassword);

                    return Json(
                              new
                              {
                                  TipoMensagem = TipoMensagemRetorno.Ok,
                                  Mensagem = string.Empty

                              }, "text/html", JsonRequestBehavior.AllowGet);

                }
            }
            catch (BusinessProcessException ex)
            {
                return Json(
                              new
                              {
                                  TipoMensagem = TipoMensagemRetorno.Erro,
                                  Mensagem = ex.Message

                              }, "text/html", JsonRequestBehavior.AllowGet);
            }
        }


        [Authorize]
        public ActionResult AlterarSenha()
        {
            return View();
        }


        [HttpPost]
        [Authorize]
        public JsonResult AlterarSenha(string SenhaAtual, string NovaSenha, string ConfirmarSenha)
        {

            try
            {

                using (var controleAcesso = new ControleAcesso())
                {

                    var user = controleAcesso.GetUsuarioByLogin(User.Identity.Name);

                    controleAcesso.AlterarSenha(user, SenhaAtual, NovaSenha, ConfirmarSenha);

                    return Json(
                                   new
                                   {
                                       TipoMensagem = TipoMensagemRetorno.Ok,
                                       Mensagem = string.Empty,
                                       URLDestino = Url.Action("Index", "Home")

                                   }, "text/html", JsonRequestBehavior.AllowGet);
                }

            }
            catch (BusinessProcessException ex)
            {

                return Json(
                               new
                               {
                                   TipoMensagem = TipoMensagemRetorno.Erro,
                                   Mensagem = ex.Message

                               }, "text/html", JsonRequestBehavior.AllowGet);
            }


        }



        [Authorize]
        public ActionResult LogOut()
        {
            WebHelpers.LogOut();
            return RedirectToAction("Index");
        }

        [Authorize]
        public async Task<ActionResult> VerificaUsuarioSenhaVaziaasync(string Login)
        {
            if (WebHelpers.ValidaSenhaVazia(Login))
                WebHelpers.LogOut();
            
            return View();
        }

    }
}