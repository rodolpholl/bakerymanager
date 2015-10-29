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
using BakeryManager.BackOffice.Models;

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
                            .AsEnumerable().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Read_Detail([DataSourceRequest] DataSourceRequest request, int IdProduto)
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
            return View(new FormulaModel()
            {
                IdFormula = 0,
                DataEmissao = DateTime.Now
            });
        }

        public ActionResult Editar(int Id)
        {
            using (var manterReceituario = new ManterReceituario())
            {
                var formula = manterReceituario.GetFormulaById(Id);

                return View(new FormulaModel()
                {
                    Categoria = new CategoriaProdutoModel()
                    {
                        IdCategoriaProduto = formula.Produto.Categoria.IdCategoriaProduto,
                        Nome = formula.Produto.Categoria.Nome
                    },
                    Produto = new ProdutoModel()
                    {
                        Nome = formula.Produto.Nome,
                        IdProduto = formula.Produto.IdProduto
                    },
                    IdFormula = formula.IdFormula,
                    DataEmissao = formula.DataEmissao,
                    DataFimValidade = formula.DataFimValidade,
                    EmUso = formula.EmUso,
                    Descricao = formula.Descricao,
                    IdProduto = formula.Produto.IdProduto,
                    DescricaoReceita = formula.DescricaoReceita,
                    RendimentoPadrao = formula.RendimentoPadrao
                });
            }
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
        public JsonResult GetIngredietesDisponiveis(int? IdCategoria, int? IdFormula, int[] IngredientesJaSelecionados)
        {
            using (var manterReceituario = new ManterReceituario())
            {

                var idCat = IdCategoria.HasValue ? IdCategoria.Value : 0;

                var idFormula = IdFormula.HasValue ? IdFormula.Value : 0;


                IList<int> IdSelecionados;

                if (IngredientesJaSelecionados != null)
                    IdSelecionados = IngredientesJaSelecionados.Select(x => x).ToList();
                else
                    IdSelecionados = new List<int>();



                var result = manterReceituario.GetIngredietesDisponiveis(idCat, idFormula, IdSelecionados).Select(x => new IngredienteFormulaModel()
                {
                    IdIngrediente = x.IdIngrediente,
                    Nome = string.IsNullOrWhiteSpace(x.Nome) ? x.NomeTACO : x.Nome,
                    Quantidade = 0
                }).AsEnumerable();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Criar(FormulaModel FormulaModel)
        {
            try
            {
                using (var manterReceituario = new ManterReceituario())
                {
                    var formula = new Formula()
                    {
                        DataEmissao = FormulaModel.DataEmissao,
                        DataFimValidade = FormulaModel.DataFimValidade,
                        Descricao = FormulaModel.Descricao,
                        DescricaoReceita = FormulaModel.DescricaoReceita,
                        EmUso = true,
                        Produto = manterReceituario.getProdutoById(FormulaModel.IdProduto)
                    };

                    manterReceituario.InserirFormula(formula);


                    return Json(
                                       new
                                       {
                                           TipoMensagem = TipoMensagemRetorno.Ok,
                                           Mensagem = "Receita inserida com sucesso!",
                                           URLDestino = Url.Action("Criar"),
                                           IdFormula = formula.IdFormula
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

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Editar(FormulaModel FormulaModel)
        {
            try
            {
                using (var manterReceituario = new ManterReceituario())
                {


                    var formula = manterReceituario.GetFormulaById(FormulaModel.IdFormula);

                    formula.DataEmissao = FormulaModel.DataEmissao;
                    formula.DataFimValidade = FormulaModel.DataFimValidade;
                    formula.Descricao = FormulaModel.Descricao;
                    formula.DescricaoReceita = FormulaModel.DescricaoReceita;


                    manterReceituario.AlterarFormula(formula);


                    return Json(
                            new
                            {
                                TipoMensagem = TipoMensagemRetorno.Ok,
                                Mensagem = "Receita inserida com sucesso!",
                                URLDestino = Url.Action("Index"),
                                IdFormula = formula.IdFormula
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
                using (var manterReceituario = new ManterReceituario())
                {
                    var formula = manterReceituario.GetFormulaById(Id);
                    manterReceituario.DesativarFormula(formula);


                    return Json(
                                       new
                                       {
                                           TipoMensagem = TipoMensagemRetorno.Ok,
                                           
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
                using (var manterReceituario = new ManterReceituario())
                {
                    var formula = manterReceituario.GetFormulaById(Id);
                    manterReceituario.ReativarFormula(formula);


                    return Json(
                                       new
                                       {
                                           TipoMensagem = TipoMensagemRetorno.Ok,

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

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult AtualizarFormula(int IdFormula, IngredienteFormulaModel[] Ingredientes, bool Edicao)
        {
            try
            {
                using (var manterReceituario = new ManterReceituario())
                {
                    var formula = manterReceituario.GetFormulaById(IdFormula);
                    manterReceituario.AtualizarFormula(formula, Ingredientes.Select(x => new IngredienteFormula()
                    {
                        Formula = formula,
                        Ingrediente = manterReceituario.GetIngredienteById(x.IdIngrediente),
                        Quantidade = x.Quantidade,
                        AGosto = x.Quantidade == 0
                    }).ToList());


                    return Json(
                                new
                                {
                                    TipoMensagem = TipoMensagemRetorno.Ok,
                                    Mensagem = Edicao ? "Receita alterada com sucesso!" : "Receita Incluída com sucesso!",
                                    URLDestino = Edicao ? Url.Action("Index") : Url.Action("Criar")

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