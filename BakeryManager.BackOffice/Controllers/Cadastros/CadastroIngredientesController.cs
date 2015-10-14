using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using BakeryManager.Services;
using BakeryManager.BackOffice.Models.Cadastros;
using BakeryManager.Entities;

namespace BakeryManager.BackOffice.Controllers.Cadastros
{
    public class CadastroIngredientesController : System.Web.Mvc.Controller
    {
        // GET: CadastroIngredientes
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Read([DataSourceRequest] DataSourceRequest request, string textoPesquisa)
        {
            using (var cadIngredientes = new CadastrarIngredientes())
            {
                IList<Ingrediente> result;

                if (textoPesquisa.Length >= 3)
                    result = cadIngredientes.GetListaIngredientesByFiltro(textoPesquisa);
                else
                    result = cadIngredientes.GetListaIngredientes();

                return Json(result.Select(x => new CadastroIngredientesModel()
                {
                    Abreviatura = x.Abreviatura,
                    CodigoTACO = x.CodigoTACO,
                    IdIngrediente = x.IdIngrediente,
                    Nome = x.Nome,
                    NomeTACO = x.NomeTACO,
                    Ativo = x.Ativo
                }).ToDataSourceResult(request)                             
                ,JsonRequestBehavior.AllowGet);
            }
        }
    }
}