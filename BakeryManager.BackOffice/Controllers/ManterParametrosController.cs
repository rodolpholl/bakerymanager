using BakeryManager.BackOffice.Models;
using BakeryManager.BackOffice.Models.Cadastros;
using BakeryManager.Services.Seguranca;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BakeryManager.BackOffice.Controllers
{
    public class ManterParametrosController : Controller
    {
        // GET: Parametros
        public ActionResult Index()
        {
            using (var parametros = new ManterParametros())
            {

                var param = new ParametrosModel() { IdParametro = 1 };

                var ListaTabelaExibicao = parametros.GetListaTabelaParaExibir().Select(x => new ParametroTabelaNutricionalModel()
                {
                    Compoonente = new TabelaNutricionalModel()
                    {

                        IdTabelaNutricionalModel = x.Compoonente.IdTabelaNutricional,
                        Nome = x.Compoonente.Nome,
                        UnidadeMedida = x.Compoonente.UnidadeMedida
                    },
                    idParametroTabelaNutricional = x.IdParametroTabelaNutricional,
                    Parametros = param
                }).ToList();

                var ListaTabelaNaoExibicao = parametros.GetListaTabelaSemExibicao().Select(x => new TabelaNutricionalModel()
                {
                    IdTabelaNutricionalModel = x.IdTabelaNutricional,
                    Nome = x.Nome,
                    UnidadeMedida = x.UnidadeMedida
                }).ToList();

                ViewData["ListaTabelaNaoExibicao"] = ListaTabelaNaoExibicao;

               
                param.ParametrosNutricionais = ListaTabelaExibicao;

                return View(param);
            }
        }
    }
}