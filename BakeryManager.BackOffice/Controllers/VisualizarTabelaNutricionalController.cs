using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BakeryManager.Services;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using BakeryManager.BackOffice.Models.ManterReceita;

namespace BakeryManager.BackOffice.Controllers
{
    public class VisualizarTabelaNutricionalController : Controller
    {
        // GET: VisualizarTabelaNutricional
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ListarCategorias()
        {
            using (var visualizarTabelaNutricional = new VisualizarTabelaNutricional())
            {
                var listaCategoria = visualizarTabelaNutricional.GetListaCategoria().OrderBy(x => x.Nome).Select(x => new
                {
                    x.IdCategoriaProduto,
                    x.Nome
                }).ToList();

                return Json(listaCategoria, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ListarProdutoByCategoria(int IdCategoria)
        {
            using (var visualizarTabelaNutricional = new VisualizarTabelaNutricional())
            {
                var listaProduto = visualizarTabelaNutricional.GetListaProdutoByCategiria(IdCategoria).OrderBy(x => x.Nome).Select(x => new
                {
                    x.IdProduto,
                    x.Nome
                }).ToList();

                return Json(listaProduto, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ListarFormulaByProduto(int IdProduto)
        {
            using (var visualizarTabelaNutricional = new VisualizarTabelaNutricional())
            {
                var listaProduto = visualizarTabelaNutricional.GetListaFormulaByCategiria(IdProduto).OrderBy(x => x.Descricao).Select(x => new
                {
                    x.IdFormula,
                    x.Descricao
                }).ToList();

                return Json(listaProduto, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetQuantidadePadraoProduto(int IdProduto)
        {
            using (var visualizarTabelaNutricional = new VisualizarTabelaNutricional())
            {
                var produto = visualizarTabelaNutricional.GetProdutoById(IdProduto);

                return Json(produto.ProporcaoTabelaNutricional, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ViualizarDadosNutricioanis([DataSourceRequest] DataSourceRequest request, int IdFormula, int QtdSimulacao)
        {
            using (var visualizarTabelaNutricional = new VisualizarTabelaNutricional())
            {
                var listaSimulada = visualizarTabelaNutricional.ViualizarDadosNutricioanis(IdFormula, QtdSimulacao).Select(x => new TabelaNutricionalSimuladorModel()
                {
                    Nome = x.Componente.Nome,
                    IdTabelaNutricional = x.Componente.IdTabelaNutricional,
                    Quantidade = Math.Round(x.Valor, 2),
                    ValorDiario = Math.Round(x.PercValorDiario.Value, 2),
                    UnidadeMedida = x.Componente.UnidadeMedida
                }).ToList();
                return Json(listaSimulada.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
        }
    }
}