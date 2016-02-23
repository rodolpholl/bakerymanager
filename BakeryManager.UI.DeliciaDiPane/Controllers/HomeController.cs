using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BakeryManager.UI.DeliciaDiPane.Controllers
{
    public class HomeController : BaseController
    {

        public HomeController() : base()
        {
            JsonHelper.URI += "/Contato";
        }
        public ActionResult Index()
        {
            return View();
        }

    }
}