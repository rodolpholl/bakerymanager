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

                return Json(retorno.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
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
            try {
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
    }
}