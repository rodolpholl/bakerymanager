using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using BakeryManager.Services;
using BakeryManager.Entities;
using BakeryManager.BackOffice.Models.Cadastros.Produtos;
using BakeryManager.BackOffice.Models;
using BakeryManager.Infraestrutura.Helpers;

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

        public JsonResult ListarCategorias()
        {
            using (var cadProduto = new CadastroProduto())
            {
                var retorno = cadProduto.GetListaCategoria().Select(x => new CategoriaProdutoModel()
                {
                    IdCategoriaProduto = x.IdCategoriaProduto,
                    Nome = x.Nome

                }).OrderBy(x => x.Nome).ToList();

                return Json(retorno, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Read([DataSourceRequest] DataSourceRequest request, string IdCategoriaFiltro, string TextoPesquisa)
        {
            using (var cadProduto = new CadastroProduto())
            {
                var retorno = cadProduto.GetProdutoByFiltro(int.Parse(string.IsNullOrWhiteSpace(IdCategoriaFiltro) ? "0" : IdCategoriaFiltro), TextoPesquisa);

                return Json(retorno.Select(x => new ProdutoModel()
                {
                    Ativo = x.Ativo,
                    GTIN = x.GTIN,
                    IdProduto = x.IdProduto,
                    Categoria = new CategoriaProdutoModel()
                    {
                        IdCategoriaProduto = x.Categoria.IdCategoriaProduto,
                        Nome = x.Categoria.Nome
                    },
                    Nome = x.Nome,
                    PossuiTabelaNutricional = cadProduto.VerificaExistenciaFormulaAssociada(x)
                }).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Criar()
        {
            return View(new ProdutoModel());
        }

        [HttpPost]
        public JsonResult Criar(ProdutoModel pProdutoModel)
        {
            try
            {
                using (var cadProduto = new CadastroProduto())
                {
                    var prod = new Produto()
                    {
                        Ativo = true,
                        GTIN = pProdutoModel.GTIN,
                        Nome = pProdutoModel.Nome,
                        Categoria = cadProduto.GetCategoriaById(pProdutoModel.Categoria.IdCategoriaProduto)
                    };

                    cadProduto.InserirProduto(prod);

                    return Json(
                                   new
                                   {
                                       TipoMensagem = TipoMensagemRetorno.Ok,
                                       Mensagem = "Produto Inserido com sucesso!",
                                       URLDestino = Url.Action("Criar"),
                                       IdProduto = prod.IdProduto
                                   }, "text/html", JsonRequestBehavior.AllowGet);

                }
            }
            catch (Exception ex)
            {
                return Json(
                           new
                           {
                               TipoMensagem = TipoMensagemRetorno.Erro,
                               Mensagem = ex.Message

                           }, "text/html", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Editar(int Id)
        {
            using (var cadProduto = new CadastroProduto())
            {
                var prod = cadProduto.GetProdutoById(Id);
                return View(new ProdutoModel()
                {
                    Ativo = prod.Ativo,
                    IdProduto = prod.IdProduto,
                    Nome = prod.Nome,
                    GTIN = prod.GTIN,
                    Categoria = new CategoriaProdutoModel()
                    {
                        IdCategoriaProduto = prod.Categoria.IdCategoriaProduto,
                        Nome = prod.Categoria.Nome
                    }
                    
                });
            }
        }

        [HttpPost]
        public JsonResult Editar(ProdutoModel pProdutoModel)
        {
            try
            {
                using (var cadProduto = new CadastroProduto())
                {
                    var prod = cadProduto.GetProdutoById(pProdutoModel.IdProduto);
                    prod.Nome = pProdutoModel.Nome;
                    prod.GTIN = pProdutoModel.GTIN;
                    prod.Categoria = cadProduto.GetCategoriaById(pProdutoModel.Categoria.IdCategoriaProduto);


                    cadProduto.AlterarProduto(prod);

                    return Json(
                                       new
                                       {
                                           TipoMensagem = TipoMensagemRetorno.Ok,
                                           Mensagem = "Produto Alterado com sucesso!",
                                           URLDestino = Url.Action("Index"),
                                           IdProduto = prod.IdProduto
                                       }, "text/html", JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(
                          new
                          {
                              TipoMensagem = TipoMensagemRetorno.Erro,
                              Mensagem = ex.Message

                          }, "text/html", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Desativar(int Id)
        {
            try
            {
                using (var cadProduto = new CadastroProduto())
                {
                    var prod = cadProduto.GetProdutoById(Id);
                    cadProduto.DesativarProduto(prod);


                    return Json(
                                       new
                                       {
                                           TipoMensagem = TipoMensagemRetorno.Ok,
                                           Mensagem = "Produto Desativado com sucesso!"

                                       }, "text/html", JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(
                          new
                          {
                              TipoMensagem = TipoMensagemRetorno.Erro,
                              Mensagem = ex.Message

                          }, "text/html", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Reativar(int Id)
        {
            try
            {
                using (var cadProduto = new CadastroProduto())
                {
                    var prod = cadProduto.GetProdutoById(Id);
                    cadProduto.ReativarProduto(prod);


                    return Json(
                                       new
                                       {
                                           TipoMensagem = TipoMensagemRetorno.Ok,
                                           Mensagem = "Produto Reativado com sucesso!"

                                       }, "text/html", JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(
                          new
                          {
                              TipoMensagem = TipoMensagemRetorno.Erro,
                              Mensagem = ex.Message

                          }, "text/html", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ExibirTabelaNutricional(int IdProduto)
        {
            using (var cadProduto = new CadastroProduto())
            {
                var prod = cadProduto.GetProdutoById(IdProduto);


                return Json(MVCHelper.RenderRazorViewToString(this, Url.Content("~/Views/CadastroProduto/TabelaNutricionalProduto.cshtml"), new ProdutoModel()
                {
                    Nome = prod.Nome,
                    IdProduto = prod.IdProduto
                }), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetListaFormulaProduto(int IdProduto)
        {
            using (var cadProduto = new CadastroProduto())
            {

                return Json(cadProduto.GetFormulaByProduto(IdProduto).Select(x => new
                {

                    IdFormula = x.IdFormula,
                    Descricao = x.Descricao

                }).ToList(), JsonRequestBehavior.AllowGet);

            }
        }

        public JsonResult GetTabelaNutricional(int IdFormula)
        {
            using (var cadProduto = new CadastroProduto())
            {
                var formula = cadProduto.GetFormulaById(IdFormula);
                var ListaIngredientes = cadProduto.getIngredientesFormula(formula);
                var ListaInformacoesExibicao = cadProduto.GetInformacoesNutricionaisExibicao();
                

                return Json(new object(), JsonRequestBehavior.AllowGet);
            }
        }
    }
}