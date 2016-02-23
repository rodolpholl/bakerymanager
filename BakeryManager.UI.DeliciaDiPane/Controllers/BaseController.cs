using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BakeryManager.InfraEstrutura.Helpers;
using System.Configuration;

namespace BakeryManager.UI.DeliciaDiPane.Controllers
{
    public class BaseController : Controller, IDisposable
    {
        protected JSONHelper JsonHelper { get; set; }


        public BaseController() : base()
        {
            JsonHelper = new JSONHelper(ConfigurationManager.AppSettings["EnderecoAPIRemoto"]);
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            JsonHelper.Dispose();
        }
    }
}