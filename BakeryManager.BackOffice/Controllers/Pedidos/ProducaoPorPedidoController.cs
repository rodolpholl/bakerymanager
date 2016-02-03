using BakeryManager.BackOffice.Models;
using BakeryManager.BackOffice.Models.Cadastros.Clientes;
using BakeryManager.BackOffice.Models.Cadastros.Funcionarios;
using BakeryManager.BackOffice.Models.Cadastros.Produtos;
using BakeryManager.BackOffice.Models.Pedido;
using BakeryManager.Entities;
using BakeryManager.InfraEstrutura.Helpers;
using BakeryManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace BakeryManager.BackOffice.Controllers.Pedidos
{
    public class ProducaoPorPedidoController : Controller
    {
        // GET: ProducaoPorPedido
        public ActionResult Index()
        {
            setViewData();
            return View();
        }

        private void setViewData()
        {
            using (var producaoPorPedido = new ProducaoPorPedido())
            {
                ViewData["ListaCliente"] = producaoPorPedido.GetListaCliente().OrderBy(x => x.Nome).Select(x => new SelectListItem()
                {
                    Text = string.Concat(x.Nome, " - ", x.TipoCliente == Entities.TipoCliente.Fisica ? x.CPF : x.CNPJ),
                    Value = x.IdCliente.ToString()
                }).ToList();

                ViewData["ListaProduto"] = producaoPorPedido.GetListaProduto().OrderBy(x => x.Nome).Select(x => new SelectListItem()
                {
                    Text = x.Nome,
                    Value = x.IdProduto.ToString()
                }).ToList();
            }
        }

        public JsonResult GetListaPedidosByFiltro(DateTime DataEntrega, DateTime? HorarioEntrega, int? IdCliente, string NumeroPedido)
        {
            using (var producaoPorPedido = new ProducaoPorPedido())
            {
                var listaRetorno = producaoPorPedido.GetPedidosByFiltro(DataEntrega, HorarioEntrega, IdCliente, NumeroPedido)
                    .Select(x => new PedidoModel()
                    {
                        IdPedido = x.IdPedido,
                        LocalEvento = x.LocalEvento,
                        Cliente = new ClienteModel()
                        {
                            IdCliente = x.Cliente.IdCliente,
                            Nome = x.Cliente.Nome
                        },
                        DataEvento = x.DataEvento,
                        DataHoraEntrega = x.DataHoraEntrega,
                        NumeroPedido = x.NumeroPedido

                    }).ToList();

                return Json(MVCHelper.RenderRazorViewToString(this, Url.Content("~/Views/ProducaoPorPedido/PedidosProducaoPartial.cshtml"), listaRetorno), JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult ProduzirPedido(int Id)
        {
            using (var producaoPorPedido = new ProducaoPorPedido())
            {

                ViewData["ListaProduto"] = producaoPorPedido.GetListaProduto().Select(x => new ProdutoModel()
                {
                    IdProduto = x.IdProduto,
                    Nome = x.Nome
                }).ToList();

                var listaRetorno = producaoPorPedido.GetProdutosProducaoByPedido(Id);

                return View(listaRetorno.Select(x => new ProducaoVisaoPedidoModel()
                {
                    Pedido = new PedidoModel()
                    {
                        IdPedido = x.Pedido.IdPedido,
                        NumeroPedido = x.Pedido.NumeroPedido
                    },
                    Produto = new ProdutoModel()
                    {
                        IdProduto = x.Produto.IdProduto,
                        Nome = x.Produto.Nome
                    },
                    Quantidade = x.Quantidade,
                    DataHoraFinalProducao = x.DataHoraFimFabricacao,
                    DataHoraIninioProducao = x.DataHoraInicioFabricacao,
                    TempoProducao = x.TempoProducao,
                    StatusAtual = (StatusProducaoProdutoModel)((int)x.StatusAtual)

                }).ToList());

            }
        }

        public JsonResult InicarProducao(int IdProduto, int IdPedido, int Quantidade)
        {
            try
            {
                using (var producaoPorPedido = new ProducaoPorPedido())
                {
                    producaoPorPedido.IniciarProducaoPedido(IdProduto, IdPedido, Quantidade, User.Identity.Name, Request.ServerVariables["REMOTE_ADDR"]);
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

        public JsonResult PausarProducao(int IdProduto, int IdPedido, int TempoDecorrido)
        {
            try
            {
                using (var producaoPorPedido = new ProducaoPorPedido())
                {
                    producaoPorPedido.PausarProducao(IdProduto, IdPedido, TempoDecorrido, User.Identity.Name, Request.ServerVariables["REMOTE_ADDR"]);
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
        public JsonResult ContinuarProducao(int IdProduto, int IdPedido)
        {
            try
            {
                using (var producaoPorPedido = new ProducaoPorPedido())
                {
                    var produtopedidoProducao = producaoPorPedido.PausarProducao(IdProduto, IdPedido, User.Identity.Name, Request.ServerVariables["REMOTE_ADDR"]);
                    return Json(new { TipoMensagem = TipoMensagemRetorno.Ok, TempoDecorrido = produtopedidoProducao.TempoProducao }, "text/html", JsonRequestBehavior.AllowGet);
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

        public JsonResult FinalizarProducao(int IdProduto, int IdPedido)
        {
            try
            {
                using (var producaoPorPedido = new ProducaoPorPedido())
                {
                    producaoPorPedido.FinalizarProducao(IdProduto, IdPedido, User.Identity.Name, Request.ServerVariables["REMOTE_ADDR"]);
                    return Json(new { TipoMensagem = TipoMensagemRetorno.Ok }, "text/html", JsonRequestBehavior.AllowGet);
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

        public JsonResult CancelarProducao(int IdProduto, int IdPedido)
        {
            try
            {
                using (var producaoPorPedido = new ProducaoPorPedido())
                {
                    producaoPorPedido.CancelarProducao(IdProduto, IdPedido, User.Identity.Name, Request.ServerVariables["REMOTE_ADDR"]);
                    return Json(new { TipoMensagem = TipoMensagemRetorno.Ok }, "text/html", JsonRequestBehavior.AllowGet);
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
        public JsonResult SairTelaProducaoPorProduto(string strProdutos)
        {

            var listaProduto = JsonConvert.DeserializeObject<IList<ProducaoVisaoPedidoModel>>(strProdutos);

            using (var producaoPorPedido = new ProducaoPorPedido())
            {

                foreach(var produtoProduzidoModel in listaProduto)
                {
                    var ProdutoProduzido = new PedidoProdutoProduzido()
                    {
                        Pedido = producaoPorPedido.GetPedidoById(produtoProduzidoModel.Pedido.IdPedido),
                        Produto = producaoPorPedido.GetProdutoById(produtoProduzidoModel.Produto.IdProduto),
                        Quantidade = produtoProduzidoModel.Quantidade,
                        TempoProducao = produtoProduzidoModel.TempoProducao,
                        StatusAtual = (StatusProducaoProduto)produtoProduzidoModel.StatusAtual
                    };

                    producaoPorPedido.IncluirProducaoPedido(ProdutoProduzido);

                }

            }

            return Json(new { }, JsonRequestBehavior.AllowGet);

        }


    }

}