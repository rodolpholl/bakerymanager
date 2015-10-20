using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BakeryManager.BackOffice.Controllers.Cadastros
{
    [Authorize]
    public class CadastroProdutoController : Controller
    {
        // GET: CadastroProduto
        public ActionResult Index()
        {
            return View();
        }
    }
}