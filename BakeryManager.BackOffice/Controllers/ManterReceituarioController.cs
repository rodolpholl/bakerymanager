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

namespace BakeryManager.BackOffice.Controllers
{
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
    }
}