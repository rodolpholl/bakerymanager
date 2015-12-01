using BakeryManager.BackOffice.Models.Cadastros.Clientes;
using BakeryManager.Services;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BakeryManager.Entities;
using BakeryManager.BackOffice.Models;
using BakeryManager.BackOffice.Helpers;
using Newtonsoft.Json;

namespace BakeryManager.BackOffice.Controllers.Cadastros
{
    [Authorize]
    public class CadastroClienteController : Controller
    {
        // GET: CadastroCliente
        public ActionResult Index()
        {

            return View();
        }

        public JsonResult TipoCliente_filter()
        {
            return Json(new List<TipoClienteModel>() {

                new TipoClienteModel()
                {
                    Descricao = "Fisica",
                    IdTipoCliente = 1
                },
                new TipoClienteModel()
                {
                    Descricao = "Juridica",
                    IdTipoCliente = 2
                }

            }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetListaCondicaoPagamento()
        {
            using (var cadCliente = new CadastroCliente())
            {
                return Json(cadCliente.GetListaCondicaoPagamento().Select(x => new CondicaoPagamentoModel()
                {
                    Descricao = x.Descricao,
                    IdCondicaoPagamento = x.IdCondicaoPagamento
                }).ToList(), JsonRequestBehavior.AllowGet);
            }
        }




        public ActionResult Criar()
        {
            return View(new ClienteModel());
        }

        
        [HttpPost]
        public JsonResult Criar(string strCliente)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var cadCliente = new CadastroCliente())
                    {


                        var clienteModel = JsonConvert.DeserializeObject<ClienteModel>(strCliente);

                        var cliente = new Cliente()
                        {
                            Ativo = true,
                            Bairro = clienteModel.Bairro.ToUpper(),
                            CEP = clienteModel.CEP.ToUpper(),
                            Cidade = clienteModel.Cidade.ToUpper(),
                            CNPJ = clienteModel.CNPJ,
                            CPF = clienteModel.CPF,
                            Complemento = clienteModel.Complemento.ToUpper(),
                            Logradouro = clienteModel.Logradouro.ToUpper(),
                            Nome = clienteModel.Nome.ToUpper(),
                            Numero = clienteModel.Numero.ToUpper(),
                            RazaoSocial = clienteModel.RazaoSocial.ToUpper(),
                            UF = clienteModel.UF.ToUpper(),
                            DataAniversario = clienteModel.DataAniversario,
                            TipoCliente = (TipoCliente)clienteModel.IdTipoCliente,
                            CondicaoPagamentoPreferencial = new CondicaoPagamento()
                            {
                                IdCondicaoPagamento = clienteModel.IdCondicaoPagamento

                            }
                        };

                        cadCliente.InserirFornecedor(cliente);


                        return Json(new
                        {
                            TipoMensagem = TipoMensagemRetorno.Ok,
                            Mensagem = "Fornecedor Inserido com sucesso!",
                            IdCliente = cliente.IdCliente
                        }, "text/html", JsonRequestBehavior.AllowGet);
                    }

                }
                catch (Exception ex)
                {
                    return Json(new
                    {
                        TipoMensagem = TipoMensagemRetorno.Erro,
                        Mensagem = ex.Message,
                    }, "text/html", JsonRequestBehavior.AllowGet);
                }

            }
            else
            {

                return Json(new
                {
                    TipoMensagem = TipoMensagemRetorno.Erro,
                    Mensagem = WebHelpers.GetErrorsAsString(ModelState),
                }, "text/html", JsonRequestBehavior.AllowGet);

            }
        }



        public ActionResult Editar(int Id)
        {
            using (var cadCliente = new CadastroCliente())
            {
                var cliente = cadCliente.GetClienteById(Id);

                return View(new ClienteModel()
                {
                    Ativo = cliente.Ativo,
                    Bairro = cliente.Bairro,
                    RazaoSocial = cliente.RazaoSocial,
                    IdCondicaoPagamento = cliente.CondicaoPagamentoPreferencial.IdCondicaoPagamento,
                    IdTipoCliente = (int)cliente.TipoCliente,
                    CEP = cliente.CEP,
                    Cidade = cliente.Cidade,
                    TipoCliente = new TipoClienteModel()
                    {
                        Descricao = Enum.GetName(typeof(TipoCliente), cliente.TipoCliente),
                        IdTipoCliente = (int)cliente.TipoCliente
                    },
                    CNPJ = cliente.CNPJ,
                    CPF = cliente.CPF,
                    Complemento = cliente.Complemento,
                    CondicaoPagamentoPreferencial = new CondicaoPagamentoModel()
                    {
                        Descricao = cliente.CondicaoPagamentoPreferencial.Descricao,
                        IdCondicaoPagamento = cliente.CondicaoPagamentoPreferencial.IdCondicaoPagamento
                    },
                    DataAniversario = cliente.DataAniversario,
                    IdCliente = cliente.IdCliente,
                    Logradouro = cliente.Logradouro,
                    Nome = cliente.Nome,
                    Numero = cliente.Numero,
                    UF = cliente.UF
                });
            }
        }


        [HttpPost]
        public JsonResult Editar(string strCliente)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var cadCliente = new CadastroCliente())
                    {
                        var clienteModel = JsonConvert.DeserializeObject<ClienteModel>(strCliente);

                        var cliente = cadCliente.GetClienteById(clienteModel.IdCliente);

                        cliente.Bairro = clienteModel.Bairro.ToUpper();
                        cliente.CEP = clienteModel.CEP.ToUpper();
                        cliente.Cidade = clienteModel.Cidade.ToUpper();
                        cliente.CNPJ = clienteModel.CNPJ;
                        cliente.CPF = clienteModel.CPF;
                        cliente.Complemento = clienteModel.Complemento.ToUpper();
                        cliente.Logradouro = clienteModel.Logradouro.ToUpper();
                        cliente.Nome = clienteModel.Nome.ToUpper();
                        cliente.Numero = clienteModel.Numero.ToUpper();
                        cliente.RazaoSocial = clienteModel.RazaoSocial.ToUpper();
                        cliente.UF = clienteModel.UF.ToUpper();
                        cliente.DataAniversario = clienteModel.DataAniversario;
                        cliente.TipoCliente = (TipoCliente)clienteModel.IdTipoCliente;
                        cliente.CondicaoPagamentoPreferencial = new CondicaoPagamento()
                        {
                            IdCondicaoPagamento = clienteModel.IdCondicaoPagamento

                        };


                        cadCliente.AlterarCliente(cliente);

                        return Json(new
                        {
                            TipoMensagem = TipoMensagemRetorno.Ok,
                            Mensagem = "Fornecedor Inserido com sucesso!",
                            IdCliente = cliente.IdCliente
                        }, "text/html", JsonRequestBehavior.AllowGet);

                    }
                }
                catch (Exception ex)
                {
                    return Json(new
                    {
                        TipoMensagem = TipoMensagemRetorno.Erro,
                        Mensagem = ex.Message,
                    }, "text/html", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new
                {
                    TipoMensagem = TipoMensagemRetorno.Erro,
                    Mensagem = WebHelpers.GetErrorsAsString(ModelState),
                }, "text/html", JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult Read([DataSourceRequest] DataSourceRequest request, int? IdTipoCliente)
        {
            using (var cadCliente = new CadastroCliente())
            {

                var Listacliente = cadCliente.GetListaClienteByTipoCliente(IdTipoCliente).Select(x => new ClienteModel()
                {
                    IdCliente = x.IdCliente,
                    Ativo = x.Ativo,
                    Bairro = x.Bairro,
                    Cidade = x.Cidade,
                    UF = x.UF,
                    Nome = x.Nome,
                    TipoCliente = new TipoClienteModel()
                    {
                        Descricao = Enum.GetName(typeof(TipoCliente), x.TipoCliente),
                        IdTipoCliente = (int)x.TipoCliente
                    }

                }).ToList();

                return Json(Listacliente.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);

            }
        }

        public ActionResult Desativar(int Id)
        {
            try
            {

                using (var cadClie = new CadastroCliente())
                {

                    cadClie.DesativarCliente(cadClie.GetClienteById(Id));

                    return Json(new
                    {
                        TipoMensagem = TipoMensagemRetorno.Ok,
                        Mensagem = "Cliente Desativado com sucesso!",
                    }, "text/html", JsonRequestBehavior.AllowGet);

                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    TipoMensagem = TipoMensagemRetorno.Erro,
                    Mensagem = ex.Message,
                }, "text/html", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Reativar(int Id)
        {
            try
            {

                using (var cadClie = new CadastroCliente())
                {

                    cadClie.ReativarCliente(cadClie.GetClienteById(Id));

                    return Json(new
                    {
                        TipoMensagem = TipoMensagemRetorno.Ok,
                        Mensagem = "Cliente Reativado com sucesso!",
                    }, "text/html", JsonRequestBehavior.AllowGet);

                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    TipoMensagem = TipoMensagemRetorno.Erro,
                    Mensagem = ex.Message,
                }, "text/html", JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult GetContatoCliente([DataSourceRequest] DataSourceRequest request, int IdCliente)
        {
            using (var cadClie = new CadastroCliente())
            {
                return Json(cadClie.GetContatosByCliente(IdCliente).Select(x => new ClienteContatoModel()
                {
                    Email = x.Email,
                    IdClienteContato = x.IdClienteContato,
                    Nome = x.Nome,
                    Site = x.Site,
                    Telefone = x.Telefone
                }).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult AtualizarContatos(IList<ClienteContato> contatos, int IdCliente)
        {
            try
            {


                using (var cadCliente = new CadastroCliente())
                {

                    cadCliente.AtualizarContato((contatos ?? new List<ClienteContato>()).Select(x => new ClienteContato()
                    {
                        Email = x.Email,
                        Nome = x.Nome,
                        Site = x.Site,
                        Telefone = x.Telefone
                    }).ToList(), IdCliente);

                    return Json(new
                    {
                        TipoMensagem = TipoMensagemRetorno.Ok,
                        Mensagem = "Operação realizada com sucesso.",
                    }, "text/html", JsonRequestBehavior.AllowGet);

                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    TipoMensagem = TipoMensagemRetorno.Erro,
                    Mensagem = ex.Message,
                }, "text/html", JsonRequestBehavior.AllowGet);
            }
        }
    }



}
