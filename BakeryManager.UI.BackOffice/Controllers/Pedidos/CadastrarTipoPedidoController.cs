using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using BakeryManager.Services;
using BakeryManager.BackOffice.Models.Pedido;
using BakeryManager.Entities;
using BakeryManager.BackOffice.Models;

namespace BakeryManager.BackOffice.Controllers
{
    public class CadastrarTipoPedidoController : Controller
    {
        // GET: CadastrarTipoPedido
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Read([DataSourceRequest] DataSourceRequest request)
        {
            using (var cadTipoPedido = new CadastrarTipoPedido())
            {
                return Json(cadTipoPedido.GetAll().OrderBy(x => x.Descricao).Select(x => new TipoPedidoModel()
                {
                    Ativo = x.Ativo,
                    Descricao = x.Descricao,
                    IdTipoPedido = x.IdTipoPedido
                }).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Edit([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<TipoPedidoModel> ListaTipoPedidoModel)
        {
            using (var cadTipoPedido = new CadastrarTipoPedido())
            {
                foreach (var tipo in ListaTipoPedidoModel)
                {
                    var TipoPedido = cadTipoPedido.GetTipoPedidoById(tipo.IdTipoPedido);
                    TipoPedido.Descricao = tipo.Descricao;
                    cadTipoPedido.AlterarTipoPedido(TipoPedido);
                }

                return Json(ListaTipoPedidoModel.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<TipoPedidoModel> ListaTipoPedidoModel)
        {
            using (var cadTipoPedido = new CadastrarTipoPedido())
            {
                foreach (var tipo in ListaTipoPedidoModel)
                {
                    var TipoPedido = new TipoPedido()
                    {
                        Ativo = true,
                        Descricao = tipo.Descricao
                    };

                    cadTipoPedido.InserirTipoPedido(TipoPedido);
                    tipo.IdTipoPedido = TipoPedido.IdTipoPedido;
                }

                return Json(ListaTipoPedidoModel.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Desativar(int Id)
        {
            try
            {
                using (var cadTipoPedido = new CadastrarTipoPedido())
                {
                    var TipoPedido = cadTipoPedido.GetTipoPedidoById(Id);
                    cadTipoPedido.DesativarTipoPedido(TipoPedido);


                    return Json(
                                       new
                                       {
                                           TipoMensagem = TipoMensagemRetorno.Ok,
                                           Mensagem = "Tipo de Pedido Desativado com sucesso!"

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
                using (var cadTipoPedido = new CadastrarTipoPedido())
                {
                    var TipoPedido = cadTipoPedido.GetTipoPedidoById(Id);
                    cadTipoPedido.ReativarTipoPedido(TipoPedido);


                    return Json(
                                       new
                                       {
                                           TipoMensagem = TipoMensagemRetorno.Ok,
                                           Mensagem = "Tipo de Pedido Reativado com sucesso!"

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