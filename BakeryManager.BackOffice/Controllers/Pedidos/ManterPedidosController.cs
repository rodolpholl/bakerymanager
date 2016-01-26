using BakeryManager.BackOffice.Models.Pedido;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BakeryManager.Services;
using BakeryManager.BackOffice.Models.Cadastros.Clientes;
using BakeryManager.BackOffice.Models;
using BakeryManager.Entities;
using BakeryManager.BackOffice.Models.Cadastros.Produtos;
using Newtonsoft.Json;
using BakeryManager.BackOffice.Models.Cadastros.Funcionarios;
using BakeryManager.InfraEstrutura.Helpers;

namespace BakeryManager.BackOffice.Controllers.Pedidos
{
    public class ManterPedidosController : Controller
    {
        // GET: ManterPedidos
        public ActionResult Index()
        {
            return View();
        }





        public JsonResult GetListaTipoPedido()
        {
            using (var manterPedido = new ManterPedido())
            {
                return Json(manterPedido.GetListaTipoPedido().Select(x => new TipoPedidoModel()
                {
                    Ativo = x.Ativo,
                    Descricao = x.Descricao,
                    IdTipoPedido = x.IdTipoPedido
                }).OrderBy(x => x.Descricao).ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ListarCategoriasProduto()
        {
            using (var manterPedido = new ManterPedido())
            {
                return Json(manterPedido.getListaCategoriaProduto().Select(x => new CategoriaProdutoModel()
                {
                    IdCategoriaProduto = x.IdCategoriaProduto,
                    Nome = x.Nome
                }).OrderBy(x => x.Nome).ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetProdutoByCategoria(int? IdCategoriaProduto)
        {
            using (var manterPedido = new ManterPedido())
            {
                return Json(manterPedido.GetProdutoByCategoria(IdCategoriaProduto).Select(x => new ProdutoModel()
                {
                    IdProduto = x.IdProduto,
                    Nome = x.Nome,
                    PrecoVenda = x.PrecoVenda
                }).OrderBy(x => x.Nome).ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetListaCondicaoPagamento()
        {
            using (var manterPedido = new ManterPedido())
            {
                return Json(manterPedido.GetListaCondicaoPagamento().Select(x => new CondicaoPagamentoModel()
                {
                    Descricao = x.Descricao,
                    IdCondicaoPagamento = x.IdCondicaoPagamento
                }).OrderBy(x => x.Descricao).ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetListaFuncionario()
        {
            using (var manterPedido = new ManterPedido())
            {
                var listaFuncionario = manterPedido.GetListaFuncionarios();
                return Json(listaFuncionario.Select(x => new FuncionarioModel()
                {
                    Nome = x.Nome,
                    IdFuncionario = x.IdFuncionario
                }), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetListaTipoContato()
        {
            using (var manterPedido = new ManterPedido())
            {
                var ListaTIpoContato = Enum.GetNames(typeof(TipoContato)).Select(x => new TipoContatoModel()
                {
                    Descricao = x,
                    IdTipoContato = (int)Enum.Parse(typeof(TipoContato), x)
                }).ToList();

                return Json(ListaTIpoContato, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetLitaNaturezaPedido()
        {
            using (var manterPedido = new ManterPedido())
            {
                var ListaNaturezaPedido = Enum.GetNames(typeof(NaturezaPedido)).Select(x => new NaturezaPedidoModel()
                {
                    Descricao = x,
                    IdNaturezaPedido = (int)Enum.Parse(typeof(NaturezaPedido), x)
                }).ToList();

                return Json(ListaNaturezaPedido, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetProdutosByPedido([DataSourceRequest] DataSourceRequest request, int? IdPedido)
        {
            using (var manterPedido = new ManterPedido())
            {
                return Json(manterPedido.GetProdutosByPedido(IdPedido.HasValue ? IdPedido.Value : 0).Select(x => new PedidoProdutoModel()
                {
                    IdProduto = x.Produto.IdProduto,
                    NomeProduto = x.Produto.Nome,
                    PrecoUnitario = x.PrecoUnitario,
                    Quantidade = x.Quantidade
                }).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
        }



        public JsonResult GetListaCliente([DataSourceRequest] DataSourceRequest request)
        {
            using (var manterPedido = new ManterPedido())
            {

                var listaClientes = manterPedido.GetListaClientesByFiltro().Select(x => new ClienteModel()
                {

                    Nome = x.TipoCliente == Entities.TipoCliente.Fisica ? string.Concat(x.Nome, " - ", x.CPF) :
                                                                          string.Concat(x.Nome, " - ", x.CNPJ),
                    IdCliente = x.IdCliente
                }).ToList();

                return Json(listaClientes, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetProdutoById(int IdProduto)
        {
            using (var manterPedido = new ManterPedido())
            {
                var prod = manterPedido.GetProdutoById(IdProduto);
                return Json(new ProdutoModel()
                {
                    IdProduto = prod.IdProduto,
                    Nome = prod.Nome,
                    PrecoVenda = prod.PrecoVenda
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetListaMaterialAdiconal(int?[] ListaMaterialSelect)
        {
            using (var manterPedido = new ManterPedido())
            {
                var listaMaterial = manterPedido.GetListaMaterialAdiconal(ListaMaterialSelect).Select(x => new MaterialAdicionalModel()
                {
                    Ativo = x.Ativo,
                    Descricao = x.Descricao,
                    IdMaterialAdicional = x.IdMaterialAdicional
                }).ToList();

                return Json(listaMaterial, JsonRequestBehavior.AllowGet);

            }
        }

        public JsonResult GetMaterialAdicionalByPedido([DataSourceRequest] DataSourceRequest request, int IdPedido, int? IdTipoPedido, bool IgnoraIdPedido)
        {
            using (var manterPedido = new ManterPedido())
            {
                var retorno = manterPedido.GetMaterialAdicionalByPedido(IdPedido, IdTipoPedido ?? 0, IgnoraIdPedido)
                    .Select(x => new PedidoMaterialAdiconalModel()
                    {
                        Material = new MaterialAdicionalModel()
                        {
                            Ativo = x.Material.Ativo,
                            Descricao = x.Material.Descricao,
                            IdMaterialAdicional = x.Material.IdMaterialAdicional
                        },
                        PrecoTotal = x.PrecoTotal,
                        PrecoUnitario = x.PrecoUnitario,
                        Quantidade = x.Quantidade,
                        TipoAquisicao = new TipoAquisicaoTemporariaModel()
                        {
                            IdTipoAquisicaoTemporaria = (int)Enum.Parse(typeof(TipoAquisicaoTemporaria), Enum.GetName(typeof(TipoAquisicaoTemporaria), x.TipoAquisicao)),
                            Nome = Enum.GetName(typeof(TipoAquisicaoTemporaria), x.TipoAquisicao)
                        }
                    }).ToList();



                return Json(retorno.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Create()
        {
            ViewData["ListaTipoAquisicao"] = Enum.GetNames(typeof(TipoAquisicaoTemporaria)).Select(x => new TipoAquisicaoTemporariaModel()
            {
                Nome = x,
                IdTipoAquisicaoTemporaria = (int)Enum.Parse(typeof(TipoAquisicaoTemporaria), x)
            }).ToList();

            using (var manterPedido = new ManterPedido())
            {
                ViewData["ListaMaterialAdicional"] = manterPedido.GetListaMaterialAdiconal(null).Select(x => new MaterialAdicionalModel()
                {
                    Descricao = x.Descricao,
                    Ativo = x.Ativo,
                    IdMaterialAdicional = x.IdMaterialAdicional
                }).ToList();
            }

            return View(new PedidoModel() { DataEvento = DateTime.Now, PrecoVenda = 0.01, IdPedido = 0 });
        }

        [HttpPost]
        public JsonResult Create(string strPedido)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var pedidoModel = JsonConvert.DeserializeObject<PedidoModel>(strPedido);

                    using (var manterPedido = new ManterPedido())
                    {
                        var pedido = new Pedido()
                        {
                            Bairro = pedidoModel.Bairro.ToUpper(),
                            CEP = pedidoModel.CEP,
                            Cidade = pedidoModel.Cidade.ToUpper(),
                            Cliente = manterPedido.GetListaClienteById(pedidoModel.Cliente.IdCliente),
                            Complemento = pedidoModel.Complemento.ToUpper(),
                            CondicaoPagamento = manterPedido.GetCondicaoPagamentoById(pedidoModel.CondicaoPagamento.IdCondicaoPagamento),
                            DataEvento = pedidoModel.DataEvento,
                            DataHoraEntrega = new DateTime(pedidoModel.DataEvento.Year,
                                                           pedidoModel.DataEvento.Month,
                                                           pedidoModel.DataEvento.Day,
                                                           pedidoModel.DataHoraEntrega.Hour,
                                                           pedidoModel.DataHoraEntrega.Minute, 0),
                            FuncionarioContato = manterPedido.GetFuncionarioById(pedidoModel.FuncionarioContato.IdFuncionario),
                            PessoaResponsavel = pedidoModel.PessoaResponsavel.ToUpper(),
                            TipoContato = pedidoModel.TipoContato.IdTipoContato == 0 ? TipoContato.Site :
                                 (TipoContato)Enum.Parse(typeof(TipoContato), pedidoModel.TipoContato.IdTipoContato.ToString()),
                            LocalEvento = pedidoModel.LocalEvento.ToUpper(),
                            Logradouro = pedidoModel.Logradouro.ToUpper(),
                            Numero = pedidoModel.Numero.ToUpper(),
                            PrecoVenda = pedidoModel.PrecoVenda,
                            TelefoneResponsavel = pedidoModel.TelefoneResponsavel,
                            TipoPedido = manterPedido.GetTipoPedidoById(pedidoModel.TipoPedido.IdTipoPedido)

                        };


                        manterPedido.InserirPedido(pedido);

                        return Json(new { IdPedido = pedido.IdPedido, TipoMensagem = TipoMensagemRetorno.Ok }, JsonRequestBehavior.AllowGet);
                    }


                }
                else
                {

                    return Json(
                       new
                       {
                           TipoMensagem = TipoMensagemRetorno.Erro,
                           Mensagem = "Erro ao Inserir o Pedido. Verifique o dados informados."

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
        public JsonResult AtualizarProdutoMaterialAdicional(string ListaProduto, string ListaMateraial, int IdPedido)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    using (var manterPedido = new ManterPedido())
                    {
                        var listaProdutoModel = JsonConvert.DeserializeObject<IList<PedidoProdutoModel>>(ListaProduto);
                        var listaProduto = listaProdutoModel.Select(x => new PedidoProduto()
                        {
                            Pedido = manterPedido.GetPedidoById(IdPedido),
                            PrecoTotal = x.Quantidade * x.PrecoUnitario,
                            Produto = manterPedido.GetProdutoById(x.IdProduto),
                            Quantidade = x.Quantidade,
                            PrecoUnitario = x.PrecoUnitario,
                            Status = StatusPedidoProduto.Pendente

                        }).ToList();

                        var listaMaterialAdicional = JsonConvert.DeserializeObject<IList<PedidoMaterialAdiconalModel>>(ListaMateraial).Select(x => new PedidoMaterialAdiconal()
                        {
                            Pedido = manterPedido.GetPedidoById(IdPedido),
                            Material = manterPedido.GetMaterialAdcionalById(x.Material.IdMaterialAdicional),
                            PrecoTotal = x.Quantidade * x.PrecoUnitario,
                            Quantidade = x.Quantidade,
                            PrecoUnitario = x.PrecoUnitario,
                            TipoAquisicao = (TipoAquisicaoTemporaria)x.TipoAquisicao.IdTipoAquisicaoTemporaria
                        }).ToList();


                        manterPedido.AtualizarProdutoMaterialAdicional(listaProduto, listaMaterialAdicional, manterPedido.GetPedidoById(IdPedido));


                        return Json(
                               new
                               {
                                   TipoMensagem = TipoMensagemRetorno.Ok,
                                   Mensagem = "Pedido Inserido com sucesso!"

                               }, "text/html", JsonRequestBehavior.AllowGet);
                    }


                }
                else
                {

                    return Json(
                       new
                       {
                           TipoMensagem = TipoMensagemRetorno.Erro,
                           Mensagem = "Erro ao Inserir o Pedido. Verifique o dados informados."

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


        //Execução do processo.

        public JsonResult GetListaPedidosEncaminhados()
        {
            using (var manterPedido = new ManterPedido())
            {
                var ListaRetorno = manterPedido.GetListaPedidosEncaminhados().Select(x => new PedidoModel()
                {
                    NumeroPedido = x.NumeroPedido,
                    IdPedido = x.IdPedido,
                    Cliente = new ClienteModel()
                    {
                        Nome = x.Cliente.Nome
                    },
                    LocalEvento = x.LocalEvento,
                    DataEvento = x.DataEvento,
                    DataHoraEntrega = x.DataHoraEntrega

                }).ToList();

                return Json(MVCHelper.RenderRazorViewToString(this, Url.Content("~/Views/ManterPedidos/EditorTemplates/PedidosEncaminhadosTemplate.cshtml"), ListaRetorno), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetViewPedido(int IdPedido)
        {
            using (var manterPedido = new ManterPedido())
            {
                var pedido = manterPedido.GetPedidoById(IdPedido);

                return Json(MVCHelper.RenderRazorViewToString(this, Url.Content("~/Views/ManterPedidos/VizualizarPedido.cshtml"), ParsePedidoToPedidoModel(manterPedido, pedido)), JsonRequestBehavior.AllowGet);

            }
        }

        private static PedidoModel ParsePedidoToPedidoModel(ManterPedido manterPedido, Pedido pedido)
        {
            return new PedidoModel()
            {
                Bairro = pedido.Bairro.ToUpper(),
                CEP = pedido.CEP,
                Cidade = pedido.Cidade.ToUpper(),
                Cliente = new ClienteModel()
                {
                    Nome = pedido.Cliente.Nome,
                    IdCliente = pedido.Cliente.IdCliente
                },
                Complemento = pedido.Complemento.ToUpper(),
                CondicaoPagamento = new CondicaoPagamentoModel()
                {
                    Descricao = pedido.CondicaoPagamento.Descricao,
                    IdCondicaoPagamento = pedido.CondicaoPagamento.IdCondicaoPagamento

                },
                DataEvento = pedido.DataEvento,
                DataHoraEntrega = pedido.DataHoraEntrega,
                FuncionarioContato = new FuncionarioModel()
                {
                    IdFuncionario = pedido.FuncionarioContato.IdFuncionario,
                    Nome = pedido.FuncionarioContato.Nome
                },
                PessoaResponsavel = pedido.PessoaResponsavel.ToUpper(),
                TipoContato = new TipoContatoModel()
                {
                    Descricao = Enum.GetName(typeof(TipoContato),pedido.TipoContato),
                    IdTipoContato = (int)pedido.TipoContato
                },
                LocalEvento = (pedido.LocalEvento ?? string.Empty).ToUpper(),
                Logradouro = pedido.Logradouro.ToUpper(),
                Numero = pedido.Numero.ToUpper(),
                NumeroPedido = pedido.NumeroPedido,
                PrecoVenda = pedido.PrecoVenda,
                TelefoneResponsavel = pedido.TelefoneResponsavel,
                TipoPedido = new TipoPedidoModel()
                {
                    Ativo = pedido.TipoPedido.Ativo,
                    Descricao = pedido.TipoPedido.Descricao,
                    IdTipoPedido = pedido.TipoPedido.IdTipoPedido
                }
            };
        }
    }
}