using BakeryManager.BackOffice.Models.Cadastros;
using BakeryManager.Services.Seguranca;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BakeryManager.BackOffice.Controllers.Cadastros
{
    public class CadastroUsuariosController : Controller
    {
        // GET: CadastroUsuarios
        public ActionResult Index()
        {
            using (var cadUsuario = new CadastroUsuario())
            {
                ViewData["ListaPerfil"] = cadUsuario.GetListaPerfil();
                return View();
            }
            
        }
        

        // POST: CadastroUsuarios/Create
        [HttpPost]
        public ActionResult Create(CadastroUsuarioModel Usuario)
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

    
        [HttpPost]
        public ActionResult Edit(CadastroUsuarioModel Usuario)
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

  

        // POST: CadastroUsuarios/Delete/5
        [HttpPost]
        public ActionResult Delete(int IdUsuario)
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
