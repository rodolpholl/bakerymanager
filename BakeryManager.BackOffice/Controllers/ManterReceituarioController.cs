using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using BakeryManager.Services;
using BakeryManager.Entities;
using BakeryManager.BackOffice.Models.Cadastros.Produtos;
using BakeryManager.BackOffice.Models.ManterReceita;
using BakeryManager.BackOffice.Models.Cadastros;
using BakeryManager.BackOffice.Models.Cadastros.Ingredientes;

namespace BakeryManager.BackOffice.Controllers
{
    [Authorize]
    public class ManterReceituarioController : Controller
    {
        // GET: ManterReceituario
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ListarCategorias()
        {
            using (var manterReceituario = new ManterReceituario())
            {
                return Json(manterReceituario.ListarCategorias().Select(x => new CategoriaProdutoModel()
                {
                    IdCategoriaProduto = x.IdCategoriaProduto,
                    Nome = x.Nome
                }).OrderBy(x => x.Nome), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ListarProduto(string IdCategoria)
        {
            using (var manterReceituario = new ManterReceituario())
            {
                var retorno = manterReceituario.ListarProduto(string.IsNullOrWhiteSpace(IdCategoria) ? 0 : int.Parse(IdCategoria));

                return Json(retorno
                    .Select(x => new ProdutoModel()
                    {
                        Nome = x.Nome,
                        IdProduto = x.IdProduto
                        

                    }).OrderBy(x => x.Nome).ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Read([DataSourceRequest] DataSourceRequest request, string IdCategoria, string IdProduto)
        {
            int idCateg, idProd = 0;
            int.TryParse(IdCategoria, out idCateg);
            int.TryParse(IdProduto, out idProd);

            using (var manterReceituario = new ManterReceituario())
            {


                var result = manterReceituario.GetProdutoByProdutoAndCategoria(idCateg, idProd);


                return Json(result
                            .Select(x => new ProdutoModel()
                            {
                                Categoria = new CategoriaProdutoModel()
                                {
                                    IdCategoriaProduto = x.Categoria.IdCategoriaProduto,
                                    Nome = x.Categoria.Nome
                                },
                                Nome = x.Nome,
                                IdProduto = x.IdProduto

                            })
                            .OrderBy(x => x.Categoria.Nome)
                            .OrderBy(x => x.Nome)
                            .AsEnumerable().ToDataSourceResult(request),JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Read_Detail([DataSourceRequest] DataSourceRequest request,  int IdProduto)
        {
          
            using (var manterReceituario = new ManterReceituario())
            {


                var result = manterReceituario.GetFormulasByProduto(IdProduto);


                return Json(result
                            .Select(x => new FormulaModel()
                            {
                                Categoria = new CategoriaProdutoModel()
                                {
                                    IdCategoriaProduto = x.Produto.Categoria.IdCategoriaProduto,
                                    Nome = x.Produto.Categoria.Nome
                                },
                                Produto = new ProdutoModel()
                                {
                                    Nome = x.Produto.Nome,
                                    IdProduto = x.Produto.IdProduto
                                },
                                DataEmissao = x.DataEmissao,
                                IdFormula = x.IdFormula,
                                Descricao = x.Descricao,
                                EmUso = x.EmUso
                            }).AsEnumerable().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
        }
        

        public ActionResult Criar()
        {
            return View();
        }

        

        public JsonResult GetIngredietesFormula([DataSourceRequest] DataSourceRequest request, int? IdFormula)
        {
            using (var manterReceituario = new ManterReceituario())
            {
                var result = manterReceituario.GetIngredientesFormula(!IdFormula.HasValue ? 0 : IdFormula.Value);
                return Json(result
                    .Select(x => new IngredienteFormulaModel()
                        {
                            IdIngrediente = x.Ingrediente.IdIngrediente,
                            Nome = string.IsNullOrWhiteSpace(x.Ingrediente.Nome) ? x.Ingrediente.NomeTACO : x.Ingrediente.Nome,
                            Quantidade = x.Quantidade
                        
                    }).AsEnumerable()
                    .ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ListarCategoriasIngrediente()
        {
            using (var manterReceituario = new ManterReceituario())
            {
                var result = manterReceituario.GetCategoriaIngredientes();
                return Json(result
                    .Select(x => new CategoriaIngredienteModel()
                    {
                        IdCategoriaIngrediente = x.IdCategoriaIngrediente,
                        Nome = x.Nome

                    }).OrderBy(x => x.Nome).ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GetIngredietesDisponiveis(int? IdCategoria, int[] IngredientesJaSelecionados)
        {
            using (var manterReceituario = new ManterReceituario())
            {

                var idCat = IdCategoria.HasValue ? IdCategoria.Value : 0;


                IList<int> IdSelecionados;

                if (IngredientesJaSelecionados != null)
                    IdSelecionados = IngredientesJaSelecionados.Select(x => x).ToList();
                else
                    IdSelecionados = new List<int>();

                

                var result = manterReceituario.GetIngredietesDisponiveis(idCat, IdSelecionados).Select(x => new IngredienteFormulaModel()
                {
                    IdIngrediente = x.IdIngrediente,
                    Nome = string.IsNullOrWhiteSpace(x.Nome) ? x.NomeTACO : x.Nome,
                    Quantidade = 0
                }).AsEnumerable();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }


    }
}