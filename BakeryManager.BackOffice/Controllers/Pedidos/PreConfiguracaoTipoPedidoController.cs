using BakeryManager.BackOffice.Models.Pedido;
using BakeryManager.Services;
using BakeryManager.Entities;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BakeryManager.BackOffice.Controllers.Pedidos
{
    public class PreConfiguracaoTipoPedidoController : Controller
    {
        // GET: PreConfiguracaoTipoPedido
        public ActionResult Index()
        {
            var ListaTipoAquisicao = Enum.GetNames(typeof(TipoAquisicaoTemporaria)).Select(x => new TipoAquisicaoTemporariaModel()
            {
                Nome = x,
                IdTipoAquisicaoTemporaria = (int)Enum.Parse(typeof(TipoAquisicaoTemporaria), x)
            }).ToList();

            ViewData["ListaTipoAquisicao"] = ListaTipoAquisicao;

            using (var preConfig = new PreConfiguracaoTipoPedido())
            {
                ViewData["ListaMaterial"] = preConfig.GetListaMateriais(null).Select(x => new MaterialAdicionalModel()
                {
                    Ativo = x.Ativo,
                    Descricao = x.Descricao,
                    IdMaterialAdicional = x.IdMaterialAdicional
                }).ToList();
            }


            return View();
        }
        public JsonResult GetListaTipoPedido()
        {
            using (var preConfig = new PreConfiguracaoTipoPedido())
            {
                return Json(preConfig.GetListaTipoPedido().Select(X => new TipoPedidoModel()
                {
                    Ativo = X.Ativo,
                    Descricao = X.Descricao,
                    IdTipoPedido = X.IdTipoPedido
                }).ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Read([DataSourceRequest] DataSourceRequest request, int? IdTipoPedido)
        {
            using (var preConfig = new PreConfiguracaoTipoPedido())
            {
                var listaPreConfig = preConfig.GetPreConfiguracaoByTipoPedido(IdTipoPedido.HasValue ? IdTipoPedido.Value : 0).Select(x => new PedidoMaterialAdicionalPreConfigModel()
                {
                    Evento = new TipoPedidoModel()
                    {
                        Ativo = x.Evento.Ativo,
                        Descricao = x.Evento.Descricao,
                        IdTipoPedido = x.Evento.IdTipoPedido
                    },
                    IdPedidoMaterialAdicionalPreConfig = x.IdPedidoMaterialAdicionalPreConfig,
                    Material = new MaterialAdicionalModel()
                    {
                        Ativo = x.Material != null ? x.Material.Ativo : false,
                        Descricao = x.Material != null ? x.Material.Descricao : "Selecione",
                        IdMaterialAdicional = x.Material != null ? x.Material.IdMaterialAdicional : 0
                    },
                    Quantidade = x.Quantidade,
                    TipoAquisicao = new TipoAquisicaoTemporariaModel()
                    {
                        Nome = Enum.GetName(typeof(TipoAquisicaoTemporaria), x.TipoAquisicao),
                        IdTipoAquisicaoTemporaria = (int)x.TipoAquisicao
                    }

                }).ToList();

                return Json(listaPreConfig.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Edit([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<PedidoMaterialAdicionalPreConfigModel> ListaPreConfiguracao, int IdTipoPedido)
        {

            using (var preConfig = new PreConfiguracaoTipoPedido())
            {
                foreach (var conf in ListaPreConfiguracao)
                {
                    var preConf = preConfig.GetPreConfiguracaoById(conf.IdPedidoMaterialAdicionalPreConfig);
                    preConf.Quantidade = conf.Quantidade;
                    preConf.Evento = preConfig.GetTipoPedidoById(IdTipoPedido);
                    preConf.Material = preConfig.GetMaterialAdicionalById(conf.Material.IdMaterialAdicional);
                    preConf.TipoAquisicao = (TipoAquisicaoTemporaria)Enum.Parse(typeof(TipoAquisicaoTemporaria), conf.TipoAquisicao.IdTipoAquisicaoTemporaria.ToString());

                    preConfig.AtualizarPreConfiguracao(preConf);
                }
            }



            return Json(ListaPreConfiguracao.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<PedidoMaterialAdicionalPreConfigModel> ListaPreConfiguracao, int IdTipoPedido)
        {
            using (var preConfig = new PreConfiguracaoTipoPedido())
            {
                foreach (var conf in ListaPreConfiguracao)
                {
                    var preConf = new PedidoMaterialAdicionalPreConfig()
                    {
                        Evento = preConfig.GetTipoPedidoById(IdTipoPedido),
                        Material = preConfig.GetMaterialAdicionalById(conf.Material.IdMaterialAdicional),
                        IdPedidoMaterialAdicionalPreConfig = conf.IdPedidoMaterialAdicionalPreConfig,
                        Quantidade = conf.Quantidade,
                        TipoAquisicao = (TipoAquisicaoTemporaria)Enum.Parse(typeof(TipoAquisicaoTemporaria), conf.TipoAquisicao.IdTipoAquisicaoTemporaria.ToString())

                    };

                    preConfig.InserirPreConfiguracao(preConf);
                    conf.IdPedidoMaterialAdicionalPreConfig = preConf.IdPedidoMaterialAdicionalPreConfig;
                }
            }



            return Json(ListaPreConfiguracao.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult Delete([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<PedidoMaterialAdicionalPreConfigModel> ListaPreConfiguracao)
        {
            using (var preConfig = new PreConfiguracaoTipoPedido())
            {
                foreach (var conf in ListaPreConfiguracao)
                {
                    var preConf = preConfig.GetPreConfiguracaoById(conf.IdPedidoMaterialAdicionalPreConfig);
                    preConfig.DeletarPreConfiguracao(preConf);
                }
            }

            return Json(ListaPreConfiguracao.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }





    }


}
