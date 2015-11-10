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
using BakeryManager.BackOffice.Models;
using BakeryManager.InfraEstrutura.Helpers;
using BakeryManager.BackOffice.Models.Cadastros.Ingredientes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BakeryManager.BackOffice.Controllers.Cadastros
{
    [Authorize]
    public class CadastroIngredientesController : System.Web.Mvc.Controller
    {
        // GET: CadastroIngredientes
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetCategoriaIngredienteAll()
        {
            using (var cadIngredientes = new CadastroIngredientes())
            {
                return Json(cadIngredientes.GetCategoriaAll().Select(x => new CategoriaIngredienteModel()
                {
                    IdCategoriaIngrediente = x.IdCategoriaIngrediente,
                    Nome = x.Nome
                }).ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Read([DataSourceRequest] DataSourceRequest request, string textoPesquisa)
        {
            using (var cadIngredientes = new CadastroIngredientes())
            {
                IList<Ingrediente> ingredientes = cadIngredientes.GetListaIngredientesByFiltro(textoPesquisa);
                IList<TabelaNutricional> TabelaNutricional = cadIngredientes.GetTabelaNutricionalAll();


                var retorno = (from i in ingredientes

                               select new IngredientesModel()
                               {
                                   Abreviatura = i.Abreviatura,
                                   CodigoTACO = i.CodigoTACO,
                                   IdIngrediente = i.IdIngrediente,
                                   Nome = i.Nome,
                                   NomeTACO = i.NomeTACO,
                                   Ativo = i.Ativo,
                                   Categoria = new CategoriaIngredienteModel()
                                   {
                                       IdCategoriaIngrediente = i.Categoria.IdCategoriaIngrediente,
                                       Nome = i.Categoria.Nome
                                   }

                               }).AsEnumerable();

                return Json(retorno.ToDataSourceResult(request)
                , JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public JsonResult Criar(IngredientesModel IngredienteModel)
        {
            try
            {
                using (var cadCliente = new CadastroIngredientes())
                {

                    var ingrediete = new Ingrediente()
                    {
                        Abreviatura = IngredienteModel.Abreviatura,
                        Ativo = true,
                        CodigoTACO = IngredienteModel.CodigoTACO,
                        Nome = IngredienteModel.Nome,
                        NomeTACO = IngredienteModel.NomeTACO,
                        Categoria = cadCliente.GetCategoriaById(IngredienteModel.Categoria.IdCategoriaIngrediente)

                    };

                    cadCliente.InserirIngrediente(ingrediete);


                    return Json(
                                new
                                {
                                    TipoMensagem = TipoMensagemRetorno.Ok,
                                    Mensagem = "Ingrediente Inserido com sucesso!",
                                    URLDestino = Url.Action("Criar"),
                                    IdIngrediente = ingrediete.IdIngrediente

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


        public ActionResult Criar()
        {
            using (var cadCliente = new CadastroIngredientes())
            {
                ViewData["ListaCategoria"] = cadCliente.GetCategoriaAll().Select(x => new CategoriaIngredienteModel()
                {
                    Nome = x.Nome,
                    IdCategoriaIngrediente = x.IdCategoriaIngrediente
                }).OrderBy(x => x.Nome).ToList();

                return View(new IngredientesModel());
            }
        }



        public ActionResult Editar(int Id)
        {
            using (var cadCliente = new CadastroIngredientes())
            {

                ViewData["ListaCategoria"] = cadCliente.GetCategoriaAll().Select(x => new CategoriaIngredienteModel()
                {
                    Nome = x.Nome,
                    IdCategoriaIngrediente = x.IdCategoriaIngrediente
                }).OrderBy(x => x.Nome).ToList();

                var ingrediente = cadCliente.GetIngredienteById(Id);


                var ingredienteModel = new IngredientesModel()
                {
                    Abreviatura = ingrediente.Abreviatura,
                    CodigoTACO = ingrediente.CodigoTACO,
                    Nome = ingrediente.Nome,
                    NomeTACO = ingrediente.NomeTACO,
                    IdIngrediente = ingrediente.IdIngrediente,
                    Categoria = new CategoriaIngredienteModel()
                    {
                        IdCategoriaIngrediente = ingrediente.Categoria.IdCategoriaIngrediente,
                        Nome = ingrediente.Categoria.Nome
                    },

                };


                return View(ingredienteModel);
            }
        }

        [HttpPost]
        public JsonResult Editar(IngredientesModel IngredienteModel)
        {
            try
            {


                using (var cadCliente = new CadastroIngredientes())
                {

                    var ingrediete = cadCliente.GetIngredienteById(IngredienteModel.IdIngrediente);

                    ingrediete.Abreviatura = IngredienteModel.Abreviatura;
                    ingrediete.Ativo = true;
                    ingrediete.CodigoTACO = IngredienteModel.CodigoTACO;
                    ingrediete.Nome = IngredienteModel.Nome;
                    ingrediete.NomeTACO = IngredienteModel.NomeTACO;
                    ingrediete.Categoria = cadCliente.GetCategoriaById(IngredienteModel.Categoria.IdCategoriaIngrediente);


                    cadCliente.AlterarIngrediente(ingrediete);




                    return Json(
                                new
                                {
                                    TipoMensagem = TipoMensagemRetorno.Ok,
                                    Mensagem = "Ingrediente Alterado com sucesso!",
                                    URLDestino = Url.Action("Index")

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



        [HttpPost]
        public JsonResult Desativar(int Id)
        {
            try
            {


                using (var cadCliente = new CadastroIngredientes())
                {

                    var ingrediente = cadCliente.GetIngredienteById(Id);
                    var user = cadCliente.GetUsuarioByLogin(User.Identity.Name);

                    cadCliente.DesativarIngrediente(ingrediente, user, Request.ServerVariables["REMOTE_ADDR"]);

                    return Json(
                                new
                                {
                                    TipoMensagem = TipoMensagemRetorno.Ok,
                                    Mensagem = "Ingrediente Desativado com sucesso!",

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

        [HttpPost]
        public JsonResult Reativar(int Id)
        {
            try
            {


                using (var cadCliente = new CadastroIngredientes())
                {

                    var ingrediente = cadCliente.GetIngredienteById(Id);
                    var user = cadCliente.GetUsuarioByLogin(User.Identity.Name);

                    cadCliente.ReativarIngrediente(ingrediente, user, Request.ServerVariables["REMOTE_ADDR"]);

                    return Json(
                                new
                                {
                                    TipoMensagem = TipoMensagemRetorno.Ok,
                                    Mensagem = "Ingrediente Reativado com sucesso!",

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

        [HttpPost]
        public JsonResult GetHistoricoDesativacaoReativacao(int IdIngrediente)
        {
            using (var cadIngrediente = new CadastroIngredientes())
            {
                var historico = cadIngrediente.GetHistoricoDesativacaoReativacaoById(IdIngrediente).Select(x => new HistoricoDesativacaoReativacaoModel()
                {
                    IdHistoricoDesativacaoReativacao = x.IdIngredienteHistoricoDesativacao,
                    DataHoraOperacao = x.DataHoraOperacao,
                    IpOperacao = x.IpOperacao,
                    TipoOperacao = x.TipoOperacao == (int)TipoOpracaoDesativacaoIngrediente.Desativar ? "Desativação" : "Reativação",
                    UsuarioAtualizacao = x.UsuarioOperacao.Nome
                }).ToList();

                return Json(MVCHelper.RenderRazorViewToString(this, Url.Content("~/Views/CadastroIngredientes/HistoricoDesativarReativar.cshtml"), historico), JsonRequestBehavior.AllowGet);


            }
        }

        [HttpPost]
        public JsonResult GetTabelaNutricional([DataSourceRequest] DataSourceRequest request, int IdIngrediente)
        {

            using (var cadIngrediente = new CadastroIngredientes())
            {

                var ListaComponentesNutricionais = cadIngrediente.GetInformacaoNutricional(IdIngrediente);

                if (IdIngrediente == 0)
                {
                    return Json(ListaComponentesNutricionais.Select(x => new IngredienteTabelaNutricionalModel()
                    {
                        Ingrediente = new IngredientesModel(),
                        ComponenteNutricional = new TabelaNutricionalModel()
                        {
                            IdTabelaNutricionalModel = x.Componente.IdTabelaNutricional,
                            Nome = x.Componente.Nome,
                            UnidadeMedida = x.Componente.UnidadeMedida
                        },
                        Valor = 0
                    }).ToList().ToTreeDataSourceResult(request), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(ListaComponentesNutricionais.Select(x => ParseIngredienteTabelaNutricionalModel(x)).ToTreeDataSourceResult(request), JsonRequestBehavior.AllowGet);
                }
            }
        }

        private IngredienteTabelaNutricionalModel ParseIngredienteTabelaNutricionalModel(IngredienteTabelaNutricional pIngrediente)
        {
            return new IngredienteTabelaNutricionalModel()
            {
                IdIngredienteTabelaNutricional = pIngrediente.IdIngredienteTabelaNutricional,
                Valor = pIngrediente.Valor,
                Ingrediente = new IngredientesModel()
                {
                    IdIngrediente = pIngrediente.Ingrediente.IdIngrediente,
                    Abreviatura = pIngrediente.Ingrediente.Abreviatura,
                    Nome = pIngrediente.Ingrediente.Nome,
                    Ativo = pIngrediente.Ingrediente.Ativo,
                    Categoria = new CategoriaIngredienteModel()
                    {
                        IdCategoriaIngrediente = pIngrediente.Ingrediente.Categoria.IdCategoriaIngrediente,
                        Nome = pIngrediente.Ingrediente.Categoria.Nome
                    },
                    CodigoTACO = pIngrediente.Ingrediente.CodigoTACO,
                    NomeTACO = pIngrediente.Ingrediente.NomeTACO

                },
                ComponenteNutricional = new TabelaNutricionalModel()
                {
                    IdTabelaNutricionalModel = pIngrediente.Componente.IdTabelaNutricional,
                    Nome = pIngrediente.Componente.Nome,
                    UnidadeMedida = pIngrediente.Componente.UnidadeMedida
                }

            };
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult UpdateTabelaNutricional(string strListaComponentes, int IdIngrediente)
        {

            using (var cadIngrediente = new CadastroIngredientes())
            {

                var ListaComponentes = JsonConvert.DeserializeObject<IEnumerable<IngredienteTabelaNutricionalModel>>(strListaComponentes);

                var ListaIngredienteTabela = ListaComponentes.Select(x => new IngredienteTabelaNutricional()
                {
                    IdIngredienteTabelaNutricional = x.IdIngredienteTabelaNutricional,
                    Componente = cadIngrediente.GetTabelaNutricionalById(x.ComponenteNutricional.IdTabelaNutricionalModel),
                    Ingrediente = cadIngrediente.GetIngredienteById(IdIngrediente),
                    Valor = x.Valor
                    
                }).ToList();


                cadIngrediente.AtualizarTabelaNutricional(ListaIngredienteTabela);


                return Json(new
                {
                    TipoMensagem = TipoMensagemRetorno.Ok,
                    Mensagem = "Ingrediente Alterado com sucesso!",
                    URLDestino = Url.Action("Index")

                }, "text/html", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult GetPainelTabelaNutricional(int IdIngrediente)
        {
            using (var cadIngrediente = new CadastroIngredientes())
            {
                var retorno = cadIngrediente.GetInformacaoNutricional(IdIngrediente).Select(x => ParseIngredienteTabelaNutricionalModel(x)).AsEnumerable();
                return Json(MVCHelper.RenderRazorViewToString(this, Url.Content("~/Views/CadastroIngredientes/TabelaNutricional.cshtml"), retorno), JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult CarregarArquivo()
        {
            try {
                using (var cadIngrediente = new CadastroIngredientes())
                {
                    var file = Request.Files["Filedata"];
                    var path = Server.MapPath(string.Concat("~\\Content\\uploads\\", file.FileName));
                    file.SaveAs(path);
                    cadIngrediente.CarregarTabelaNutricional(path);
                    return Content("Arquivo Salvo com Sucesso!");
                }
            }

            catch (Exception ex)
            {
                return Content(ex.Message);
            }

        }
    }
}

