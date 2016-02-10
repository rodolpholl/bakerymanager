using BakeryManager.BackOffice.Models;
using BakeryManager.BackOffice.Models.Cadastros.Produtos;
using BakeryManager.BackOffice.Models.Pedido;
using BakeryManager.Entities;
using BakeryManager.InfraEstrutura.Helpers;
using BakeryManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Globalization;

namespace BakeryManager.BackOffice.Controllers.Pedidos
{
    public class ProducaoPorProdutoController : Controller
    {
        // GET: ProducaoPorProduto
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetListaProdutosProducao(string DataEntrega, string HoraEntrega)
        {

            var dtEntrega = DateTime.ParseExact(DataEntrega, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            DateTime? hrEntrega = null;

            if (!string.IsNullOrWhiteSpace(HoraEntrega))
                hrEntrega = DateTime.ParseExact(HoraEntrega, "HH:mm", CultureInfo.InvariantCulture);

            using (var producaoVisaoProduto = new ProducaoPorProduto())
            {
                var listaRetorno = producaoVisaoProduto.GetListaProdutoEmProducao(dtEntrega, hrEntrega).Select(x => new ProducaoVisaoProdutoModel()
                {
                    Produto = new ProdutoModel()
                    {
                        IdProduto = x.Produto.IdProduto,
                        Nome = x.Produto.Nome
                    },
                    Quantidade = x.Quantidade,
                    StatusAtual = (StatusProducaoProdutoModel)x.StatusAtual,
                    DataHoraFinalProducao = x.DataHoraFimFabricacao,
                    DataHoraIninioProducao = x.DataHoraInicioFabricacao,
                    TempoProducao = x.TempoProducao
                }).ToList();
                return Json(MVCHelper.RenderRazorViewToString(this, Url.Content("~/Views/ProducaoPorProduto/ProuzirProduto.cshtml"), listaRetorno), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult InicarProducao(int IdProduto)
        {
            
            
            try
            {
                using (var producaoVisaoProduto = new ProducaoPorProduto())
                {
                    producaoVisaoProduto.IniciarProducao(IdProduto,User.Identity.Name, Request.ServerVariables["REMOTE_ADDR"]);
                }

                return Json(new { TipoMensagem = TipoMensagemRetorno.Ok }, "text/html", JsonRequestBehavior.AllowGet);
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

        public JsonResult PausarProducao(int IdProduto, int TempoDecorrido)
        {
            
            try
            {
                using (var producaoVisaoProduto = new ProducaoPorProduto())
                {
                    producaoVisaoProduto.PausarProducao(IdProduto,TempoDecorrido,User.Identity.Name, Request.ServerVariables["REMOTE_ADDR"]);
                }

                return Json(new { TipoMensagem = TipoMensagemRetorno.Ok }, "text/html", JsonRequestBehavior.AllowGet);
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

        public JsonResult ContinuarProducao(int IdProduto)
        {
            try
            {

                using (var producaoVisaoProduto = new ProducaoPorProduto())
                {
                    var TempoDecorrido = producaoVisaoProduto.ContinuarProducao(IdProduto,User.Identity.Name, Request.ServerVariables["REMOTE_ADDR"]);

                    return Json(new { TipoMensagem = TipoMensagemRetorno.Ok, TempoDecorrido = TempoDecorrido }, "text/html", JsonRequestBehavior.AllowGet);
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

        public JsonResult FinalizarProducao(int IdProduto, int TempoDecorrido)
        {
            try
            {
                using (var producaoVisaoProduto = new ProducaoPorProduto())
                {
                     producaoVisaoProduto.FinalizarProducao(IdProduto, TempoDecorrido,User.Identity.Name, Request.ServerVariables["REMOTE_ADDR"]);

                }

                return Json(new { TipoMensagem = TipoMensagemRetorno.Ok}, "text/html", JsonRequestBehavior.AllowGet);
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

        public JsonResult CancelarProducao(int IdProduto, int TempoDecorrido)
        {
            try
            {
                using (var producaoVisaoProduto = new ProducaoPorProduto())
                {
                    producaoVisaoProduto.CancelarProducao(IdProduto, TempoDecorrido, User.Identity.Name, Request.ServerVariables["REMOTE_ADDR"]);

                }

                return Json(new { TipoMensagem = TipoMensagemRetorno.Ok }, "text/html", JsonRequestBehavior.AllowGet);
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
        public JsonResult SairTelaProducaoPorProduto(string strProdutos)
        {
            var listaProduto = JsonConvert.DeserializeObject<IList<ProducaoVisaoProdutoModel>>(strProdutos);

            using (var producaoVisaoProduto = new ProducaoPorProduto())
            {

                foreach (var produtoProduzidoModel in listaProduto)
                {
                    var ProdutoProduzido = new PedidoProdutoProduzido()
                    {
                        Produto = producaoVisaoProduto.GetProdutoById(produtoProduzidoModel.Produto.IdProduto),
                        Quantidade = produtoProduzidoModel.Quantidade,
                        TempoProducao = produtoProduzidoModel.TempoProducao,
                        StatusAtual = (StatusProducaoProduto)produtoProduzidoModel.StatusAtual
                    };

                    producaoVisaoProduto.IncluirProducaoPedido(ProdutoProduzido);

                }

            }

            return Json(new { }, JsonRequestBehavior.AllowGet);
        }
    }


}