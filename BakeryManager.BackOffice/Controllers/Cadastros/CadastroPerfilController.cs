using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc;
using Kendo.Mvc.UI;
using BakeryManager.Services.Seguranca;
using BakeryManager.BackOffice.Models.Cadastros;
using Kendo.Mvc.Extensions;

namespace BakeryManager.BackOffice.Controllers.Cadastros
{
    public class CadastroPerfilController : Controller
    {
        // GET: CadastroPerfil
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            using (var cadPerfil = new CadastroPerfil())
            {

                var listaPerfil = cadPerfil.GetListaPerfil().Select(x => new CadastroPerfilModel()
                {
                    IdPerfil = x.IdPerfil,
                    Ativo = x.Ativo,
                    AtribuicaoPerfilEnum = (AtribuicaoPerfil)int.Parse(x.Atribuicao.ToString()),
                    Nome = x.Nome
                }).ToList();

                return Json(listaPerfil.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
        }

        // GET: CadastroPerfil/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CadastroPerfil/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CadastroPerfil/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CadastroPerfil/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CadastroPerfil/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CadastroPerfil/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CadastroPerfil/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
