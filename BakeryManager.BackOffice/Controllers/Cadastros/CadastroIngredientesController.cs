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
using BakeryManager.BackOffice.Models.Cadastros.Ingredientes;

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
                IList<Ingrediente> ingredientes = cadIngredientes.GetListaIngredientesByFiltro(textoPesquisa);
                IList<TabelaNutricional> TabelaNutricional = cadIngredientes.GetTabelaNutricionalAll();


                var retorno = (from i in ingredientes
                               join tn in TabelaNutricional on
                                  i.IdIngrediente equals tn.Ingrediente.IdIngrediente into tnj
                               from t in tnj.DefaultIfEmpty()
                               select new CadastroIngredientesModel()
                               {
                                   Abreviatura = i.Abreviatura,
                                   CodigoTACO = i.CodigoTACO,
                                   IdIngrediente = i.IdIngrediente,
                                   Nome = i.Nome,
                                   NomeTACO = i.NomeTACO,
                                   Ativo = i.Ativo,
                                   TabelaNutricional = ParseTabelaNutricionalModel(t)
                               }).AsEnumerable();
                
                return Json(retorno.ToDataSourceResult(request)
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

                    cadCliente.InserirIngrediente(ingrediete, ParseTabelaNutricional(IngredienteModel.TabelaNutricional));

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
            return View(new CadastroIngredientesModel());
        }



        public ActionResult Editar(int Id)
        {
            using (var cadCliente = new CadastroIngredientes())
            {
                var ingrediente = cadCliente.GetIngredienteById(Id);
                var tabelaNutricional = cadCliente.GetTabelaNutricionalByIdIngrediente(Id);

                var ingredienteModel = new CadastroIngredientesModel()
                {
                    Abreviatura = ingrediente.Abreviatura,
                    CodigoTACO = ingrediente.CodigoTACO,
                    Nome = ingrediente.Nome,
                    NomeTACO = ingrediente.NomeTACO,
                    IdIngrediente = ingrediente.IdIngrediente,
                    TabelaNutricional = (ParseTabelaNutricionalModel(tabelaNutricional))
                };

                ingredienteModel.TabelaNutricional.Ingrediente = ingredienteModel;

                return View(ingredienteModel);
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



                    cadCliente.AlterarIngrediente(ingrediete,ParseTabelaNutricional(IngredienteModel.TabelaNutricional));

                    


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

        private TabelaNutricionalModel ParseTabelaNutricionalModel(TabelaNutricional pTabelaNutricional)
        {
            if (pTabelaNutricional == null)
            {
                return new TabelaNutricionalModel();
            }
            else
            {
                return new TabelaNutricionalModel()
                {
                    Calcio = pTabelaNutricional.Calcio,
                    Cinzas = pTabelaNutricional.Cinzas,
                    Carbidrato = pTabelaNutricional.Carbidrato,
                    Cobre = pTabelaNutricional.Cobre,
                    Colesterol = pTabelaNutricional.Colesterol,
                    EnergiaKCAL = pTabelaNutricional.EnergiaKCAL,
                    EnergiaKJ = pTabelaNutricional.EnergiaKJ,
                    Ferro = pTabelaNutricional.Ferro,
                    FibraAlimentar = pTabelaNutricional.FibraAlimentar,
                    Fosforo = pTabelaNutricional.Fosforo,
                    Lipidio = pTabelaNutricional.Lipidio,
                    Magnesio = pTabelaNutricional.Magnesio,
                    Manganes = pTabelaNutricional.Manganes,
                    Niacina = pTabelaNutricional.Niacina,
                    Piridoxina = pTabelaNutricional.Piridoxina,
                    Potassio = pTabelaNutricional.Potassio,
                    Proteina = pTabelaNutricional.Proteina,
                    RAE = pTabelaNutricional.RAE,
                    RE = pTabelaNutricional.RE,
                    Retinol = pTabelaNutricional.Retinol,
                    Riboflavina = pTabelaNutricional.Riboflavina,
                    Sodio = pTabelaNutricional.Sodio,
                    Tiamina = pTabelaNutricional.Tiamina,
                    Umidade = pTabelaNutricional.Umidade,
                    VitaminaC = pTabelaNutricional.VitaminaC,
                    Zinco = pTabelaNutricional.Zinco
                };
            }
        }

        private TabelaNutricional ParseTabelaNutricional(TabelaNutricionalModel pTabelaNutricionalModel)
        {
            if (pTabelaNutricionalModel == null)
            {
                return new TabelaNutricional();
            }
            else
            {
                return new TabelaNutricional()
                {
                    Calcio = pTabelaNutricionalModel.Calcio,
                    Cinzas = pTabelaNutricionalModel.Cinzas,
                    Carbidrato = pTabelaNutricionalModel.Carbidrato,
                    Cobre = pTabelaNutricionalModel.Cobre,
                    Colesterol = pTabelaNutricionalModel.Colesterol,
                    EnergiaKCAL = pTabelaNutricionalModel.EnergiaKCAL,
                    EnergiaKJ = pTabelaNutricionalModel.EnergiaKJ,
                    Ferro = pTabelaNutricionalModel.Ferro,
                    FibraAlimentar = pTabelaNutricionalModel.FibraAlimentar,
                    Fosforo = pTabelaNutricionalModel.Fosforo,
                    Lipidio = pTabelaNutricionalModel.Lipidio,
                    Magnesio = pTabelaNutricionalModel.Magnesio,
                    Manganes = pTabelaNutricionalModel.Manganes,
                    Niacina = pTabelaNutricionalModel.Niacina,
                    Piridoxina = pTabelaNutricionalModel.Piridoxina,
                    Potassio = pTabelaNutricionalModel.Potassio,
                    Proteina = pTabelaNutricionalModel.Proteina,
                    RAE = pTabelaNutricionalModel.RAE,
                    RE = pTabelaNutricionalModel.RE,
                    Retinol = pTabelaNutricionalModel.Retinol,
                    Riboflavina = pTabelaNutricionalModel.Riboflavina,
                    Sodio = pTabelaNutricionalModel.Sodio,
                    Tiamina = pTabelaNutricionalModel.Tiamina,
                    Umidade = pTabelaNutricionalModel.Umidade,
                    VitaminaC = pTabelaNutricionalModel.VitaminaC,
                    Zinco = pTabelaNutricionalModel.Zinco
                };
            }
        }
    }
}