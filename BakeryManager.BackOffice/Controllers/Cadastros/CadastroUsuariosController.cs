using BakeryManager.BackOffice.Controllers.Cadastros;
using BakeryManager.BackOffice.Models.Cadastros;
using BakeryManager.Entities;
using BakeryManager.Services.Seguranca;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BakeryManager.BackOffice.Controllers.Cadastros
{
    [Authorize]
    public class CadastroUsuariosController : System.Web.Mvc.Controller
    {
        // GET: CadastroUsuarios
        public ActionResult Index()
        {

            return View();


        }


        // POST: CadastroUsuarios/Create
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, CadastroUsuarioModel user)
        {
            try
            {
                // TODO: Add insert logic here


                using (var cadUsuario = new CadastroUsuario())
                {
                    var usuario = new Usuario()
                    {
                        Nome = user.Nome,
                        AutenticaSenhaDia = user.AutenticaSenhaDia,
                        DataCriacao = DateTime.Now,
                        Ativo = user.Ativo,
                        Email = user.Email,
                        Login = user.Login.ToUpper(),
                        Telefone = user.Telefone
                    };

                    cadUsuario.InserirUsuario(usuario);
                    user.IdUsuario = usuario.IdUsuario;
                }


                AtualizarPerfilInformado(user);


                return Json(new[] { user }.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return View();
            }
        }

        public JsonResult GetListaPerfil()
        {
            using (var cadUsuario = new CadastroUsuario())
            {
                return Json(cadUsuario.GetListaPerfilAtivo().Select(x => new CadastroPerfilModel()
                {
                    Ativo = x.Ativo,
                    Nome = x.Nome,
                    IdPerfil = x.IdPerfil
                }).OrderBy(x => x.Nome).ToList(), JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult Read([DataSourceRequest] DataSourceRequest request)
        {


            using (var cadUsuario = new CadastroUsuario())
            {

                var ListaUsuario = cadUsuario.GetListaUsuario().Select(x => new CadastroUsuarioModel()
                {
                    Ativo = x.Ativo,
                    Email = x.Email,
                    IdUsuario = x.IdUsuario,
                    Login = x.Login,
                    Nome = x.Nome,
                    AutenticaSenhaDia = x.AutenticaSenhaDia,
                    Telefone = x.Telefone,
                    Perfil = new CadastroPerfilModel()
                    {
                        Nome = cadUsuario.GetPerfilAtivoByUsuario(x).Nome,
                        IdPerfil = cadUsuario.GetPerfilAtivoByUsuario(x).IdPerfil,
                        Ativo = cadUsuario.GetPerfilAtivoByUsuario(x).Ativo
                    }
                }).ToList();

                return Json(ListaUsuario.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);



            }


        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit([DataSourceRequest] DataSourceRequest request, CadastroUsuarioModel user)
        {
            try
            {
                // TODO: Add insert logic here


                using (var cadUsuario = new CadastroUsuario())
                {


                    var usuario = cadUsuario.GetUsuarioById(user.IdUsuario);

                    usuario.Nome = user.Nome;
                    usuario.AutenticaSenhaDia = user.AutenticaSenhaDia;
                    usuario.DataCriacao = DateTime.Now;
                    usuario.Ativo = user.Ativo;
                    usuario.Email = user.Email;
                    usuario.Login = user.Login.ToUpper();
                    usuario.Telefone = user.Telefone;

                    cadUsuario.AlterarUsuario(usuario);


                }

                AtualizarPerfilInformado(user);

                return Json(new[] { user }.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return View();
            }
        }

        private void AtualizarPerfilInformado(CadastroUsuarioModel user)
        {
            using (var cadUsuario = new CadastroUsuario())
            {
                var usuario = cadUsuario.GetUsuarioById(user.IdUsuario);
                var perfil = cadUsuario.GetPerfilById(user.Perfil.IdPerfil);
                cadUsuario.AtualizarAssociacaoPerfil(usuario, perfil);
            }
        }

        public JsonResult Desativar(int pIdUsuario)
        {
            using (var cadUsuario = new CadastroUsuario())
            {
                var usuario = cadUsuario.GetUsuarioById(pIdUsuario);
                cadUsuario.DesativarUsuario(usuario);

                return Json(new { }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Reativar(int pIdUsuario)
        {
            using (var cadUsuario = new CadastroUsuario())
            {
                var usuario = cadUsuario.GetUsuarioById(pIdUsuario);
                cadUsuario.ReativarUsuario(usuario);

                return Json(new { }, JsonRequestBehavior.AllowGet);
            }
        }


    }
}
