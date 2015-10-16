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
using BakeryManager.Infraestrutura.Helpers;

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

        public JsonResult Read([DataSourceRequest] DataSourceRequest request, string textoPesquisa)
        {
            using (var cadIngredientes = new CadastroIngredientes())
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
                , JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public JsonResult Criar(CadastroIngredientesModel IngredienteModel)
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
                        NomeTACO = IngredienteModel.NomeTACO

                    };

                    cadCliente.InserirIngrediente(ingrediete);

                    return Json(
                                new
                                {
                                    TipoMensagem = TipoMensagemRetorno.Ok,
                                    Mensagem = "Ingrediente Inserido com sucesso!",
                                    URLDestino = Url.Action("Criar")

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
            return View();
        }



        public ActionResult Editar(int Id)
        {
            using (var cadCliente = new CadastroIngredientes())
            {
                var ingrediente = cadCliente.GetIngredienteById(Id);
                return View(new CadastroIngredientesModel()
                {
                    Abreviatura = ingrediente.Abreviatura,
                    CodigoTACO = ingrediente.CodigoTACO,
                    Nome = ingrediente.Nome,
                    NomeTACO = ingrediente.NomeTACO,
                    IdIngrediente = ingrediente.IdIngrediente
                });
            }
        }

        [HttpPost]
        public JsonResult Editar(CadastroIngredientesModel IngredienteModel)
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
    }
}