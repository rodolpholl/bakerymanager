using BakeryManager.BackOffice.Models.Cadastros.Produtos;
using BakeryManager.Services;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BakeryManager.BackOffice.Models.ManterReceita;

namespace BakeryManager.BackOffice.Controllers
{
    [Authorize]
    public class SimuladorController : Controller
    {
        // GET: Simulador
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ListarCategorias()
        {
            using (var simulador = new Simulador())
            {
                var listaCategoria = simulador.GetListaCategoria().OrderBy(x => x.Nome).Select(x => new
                {
                    x.IdCategoriaProduto,
                    x.Nome
                }).ToList();

                return Json(listaCategoria, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ListarProdutoByCategoria(int IdCategoria)
        {
            using (var simulador = new Simulador())
            {
                var listaProduto = simulador.GetListaProdutoByCategiria(IdCategoria).OrderBy(x => x.Nome).Select(x => new
                {
                    x.IdProduto,
                    x.Nome
                }).ToList();

                return Json(listaProduto, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ListarFormulaByProduto(int IdProduto)
        {
            using (var simulador = new Simulador())
            {
                var listaProduto = simulador.GetListaFormulaByCategiria(IdProduto).OrderBy(x => x.Descricao).Select(x => new
                {
                    x.IdFormula,
                    x.Descricao
                }).ToList();

                return Json(listaProduto, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetIngredietesFormula([DataSourceRequest] DataSourceRequest request, int IdFormula)
        {
            using (var simulador = new Simulador())
            {
                var listaProduto = simulador.GetIngredientesByFormula(IdFormula).Select(x => new IngredienteFormulaModel()
                {
                    AGosto = x.AGosto,
                    Nome = string.IsNullOrWhiteSpace(x.Ingrediente.Nome) ? x.Ingrediente.NomeTACO : x.Ingrediente.Nome,
                    Quantidade = x.Quantidade
                }).ToList();

                return Json(listaProduto.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SimularReceita([DataSourceRequest] DataSourceRequest request, int IdFormula, int QtdSimulacao)
        {
            using (var simulador = new Simulador())
            {
                var listaSimulada = simulador.SimularReceita(IdFormula, QtdSimulacao).Select(x => new IngredienteFormulaModel()
                {
                    AGosto = x.AGosto,
                    Nome = string.IsNullOrWhiteSpace(x.Ingrediente.Nome) ? x.Ingrediente.NomeTACO : x.Ingrediente.Nome,
                    Quantidade = x.Quantidade
                }).ToList();

                return Json(listaSimulada.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
        }
        
        public JsonResult GetQuantidadePadraoProduto(int IdProduto)
        {
            using (var simulador = new Simulador())
            {
                var produto = simulador.GetProdutoById(IdProduto);

                return Json(produto.ProporcaoTabelaNutricional, JsonRequestBehavior.AllowGet);
            }
        }
    }
}