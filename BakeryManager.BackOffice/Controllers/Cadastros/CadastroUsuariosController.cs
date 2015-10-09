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
            return View();
        }

        // GET: CadastroUsuarios/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CadastroUsuarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CadastroUsuarios/Create
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

        // GET: CadastroUsuarios/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CadastroUsuarios/Edit/5
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

        // GET: CadastroUsuarios/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CadastroUsuarios/Delete/5
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
