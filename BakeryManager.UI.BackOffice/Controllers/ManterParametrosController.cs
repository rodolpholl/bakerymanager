﻿using BakeryManager.BackOffice.Models;
using BakeryManager.BackOffice.Models.Cadastros;
using BakeryManager.Entities;
using BakeryManager.Services.Seguranca;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BakeryManager.BackOffice.Controllers
{
    [Authorize]
    public class ManterParametrosController : Controller
    {
        // GET: Parametros
        public ActionResult Index()
        {
            using (var parametros = new ManterParametros())
            {

                var param = new ParametrosModel() { IdParametro = 1 };

                var ListaTabelaExibicao = parametros.GetListaTabelaParaExibir().Select(x => new ParametroTabelaNutricionalModel()
                {
                    Compoonente = new TabelaNutricionalModel()
                    {

                        IdTabelaNutricionalModel = x.Compoonente.IdTabelaNutricional,
                        Nome = x.Compoonente.Nome,
                        UnidadeMedida = x.Compoonente.UnidadeMedida
                    },
                    idParametroTabelaNutricional = x.IdParametroTabelaNutricional,
                    Parametros = param
                }).ToList();

                var ListaTabelaNaoExibicao = parametros.GetListaTabelaSemExibicao().Select(x => new TabelaNutricionalModel()
                {
                    IdTabelaNutricionalModel = x.IdTabelaNutricional,
                    Nome = x.Nome,
                    UnidadeMedida = x.UnidadeMedida
                }).ToList();

                ViewData["ListaTabelaNaoExibicao"] = ListaTabelaNaoExibicao;


                param.ParametrosNutricionais = ListaTabelaExibicao;

                return View(param);
            }
        }

        [HttpPost]
        public JsonResult SalvarConfiguracao(IList<TabelaNutricionalModel> ListaComponentes)
        {
            try
            {
                using (var parametros = new ManterParametros())
                {
                    parametros.SalvarConfiguracao((ListaComponentes ?? new List<TabelaNutricionalModel>()).Select(x => new ParametroTabelaNutricional() {
                        Compoonente = new TabelaNutricional()
                        {

                            IdTabelaNutricional = x.IdTabelaNutricionalModel,
                            Nome = x.Nome,
                            UnidadeMedida = x.UnidadeMedida
                        },
                        Parametros = new ParametrosGerais() { IdParametrosGerais = 1}
                    }).ToList());

                    return Json(
                                new
                                {
                                    TipoMensagem = TipoMensagemRetorno.Ok,
                                    Mensagem = "Cofiguração Salva com Sucesso!!",
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

        public JsonResult ReadCondicaoPagamento([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                using (var parametros = new ManterParametros())
                {
                    
                    return Json(parametros.GetListaCondicaoPagamento().Select(x => new CondicaoPagamentoModel()
                    {
                        Descricao = x.Descricao,
                        IdCondicaoPagamento = x.IdCondicaoPagamento
                    }).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);

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

        public JsonResult SalvarCondicaoPagamento(IList<CondicaoPagamentoModel> ListaCondicao)
        {
            try
            {
                using (var parametros = new ManterParametros())
                {

                    parametros.SalvarCondicaoPagamento(ListaCondicao.Select(x => new CondicaoPagamento()
                    {
                        Descricao = x.Descricao,
                        IdCondicaoPagamento = x.IdCondicaoPagamento
                    }).ToList());

                    return Json(
                            new
                            {
                                TipoMensagem = TipoMensagemRetorno.Ok,
                                Mensagem = "Operação Realizada Com sucesso!"

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