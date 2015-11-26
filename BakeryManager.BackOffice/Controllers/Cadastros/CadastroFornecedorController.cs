using BakeryManager.BackOffice.Models.Cadastros.Fornecedores;
using BakeryManager.Services;
using BakeryManager.Entities;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BakeryManager.BackOffice.Models;
using BakeryManager.BackOffice.Helpers;

namespace BakeryManager.BackOffice.Controllers.Cadastros
{
    [Authorize]
    public class CadastroFornecedorController : Controller
    {
        // GET: CadastroFornecedor
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Read([DataSourceRequest] DataSourceRequest request)
        {
            using (var cadForn = new CadastroFornecedor())
            {
                var Result = cadForn.GetFornecedores().Select(x =>

                    new FornecedorModel()
                    {
                        Ativo = x.Ativo,
                        Bairro = x.Bairro,
                        Cidade = x.Cidade,
                        CEP = x.CEP,
                        Nome = x.Nome,
                        UF = x.UF,
                        IdFornecedor = x.IdFornecedor,
                        PrazoEntregaPrevisto = x.PrazoEntregaPrevisto,
                        QuantidadeEntregaSemana = x.QuantidadeEntregaSemana
                    }).AsEnumerable();

                return Json(Result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);

            }
        }

        public ActionResult Criar()
        {
            return View(new FornecedorModel()
            {
                Ativo = true,
                IdFornecedor = 0
            });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Criar( FornecedorModel fornecedorModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var cadForn = new CadastroFornecedor())
                    {

                        var forn = new Fornecedor()
                        {
                            Ativo = fornecedorModel.Ativo,
                            Bairro = fornecedorModel.Bairro.ToUpper(),
                            CEP = fornecedorModel.CEP.ToUpper(),
                            Cidade = fornecedorModel.Cidade.ToUpper(),
                            CNPJ = fornecedorModel.CNPJ.ToUpper(),
                            Complemento = fornecedorModel.Complemento.ToUpper(),
                            Logradouro = fornecedorModel.Logradouro.ToUpper(),
                            Nome = fornecedorModel.Nome.ToUpper(),
                            Numero = fornecedorModel.Numero.ToUpper(),
                            RazaoSocial = fornecedorModel.RazaoSocial.ToUpper(),
                            UF = fornecedorModel.UF.ToUpper(),
                            PrazoEntregaPrevisto = fornecedorModel.PrazoEntregaPrevisto,
                            QuantidadeEntregaSemana = fornecedorModel.QuantidadeEntregaSemana                            
                        };

                        cadForn.InserirFornecedor(forn);


                        return Json(new
                        {
                            TipoMensagem = TipoMensagemRetorno.Ok,
                            Mensagem = "Fornecedor Inserido com sucesso!",
                            IdFornecedor = forn.IdFornecedor
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
            using (var cadForn = new CadastroFornecedor())
            {

                var forn = cadForn.GetFornecedorById(Id);

                return View(new FornecedorModel()
                {
                    IdFornecedor = forn.IdFornecedor,
                    Bairro = forn.Bairro,
                    CEP = forn.CEP,
                    Cidade = forn.Cidade,
                    CNPJ = forn.CNPJ,
                    Complemento = forn.Complemento,
                    Logradouro = forn.Logradouro,
                    Nome = forn.Nome,
                    Numero = forn.Numero,
                    RazaoSocial = forn.RazaoSocial,
                    UF = forn.UF,
                    PrazoEntregaPrevisto = forn.PrazoEntregaPrevisto,
                    QuantidadeEntregaSemana = forn.QuantidadeEntregaSemana,
                    Ativo = true
                });


            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Editar(FornecedorModel fornecedorModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var cadForn = new CadastroFornecedor())
                    {

                        var forn = cadForn.GetFornecedorById(fornecedorModel.IdFornecedor);

              
                        forn.Bairro = fornecedorModel.Bairro.ToUpper();
                        forn.CEP = fornecedorModel.CEP.ToUpper();
                        forn.Cidade = fornecedorModel.Cidade.ToUpper();
                        forn.CNPJ = fornecedorModel.CNPJ.ToUpper();
                        forn.Complemento = fornecedorModel.Complemento.ToUpper();
                        forn.Logradouro = fornecedorModel.Logradouro.ToUpper();
                        forn.Nome = fornecedorModel.Nome.ToUpper();
                        forn.Numero = fornecedorModel.Numero.ToUpper();
                        forn.RazaoSocial = fornecedorModel.RazaoSocial.ToUpper();
                        forn.UF = fornecedorModel.UF.ToUpper();
                        forn.PrazoEntregaPrevisto = fornecedorModel.PrazoEntregaPrevisto;
                        forn.QuantidadeEntregaSemana = fornecedorModel.QuantidadeEntregaSemana;

                        cadForn.AlterarFornecedor(forn);

                        return Json(new
                        {
                            TipoMensagem = TipoMensagemRetorno.Ok,
                            Mensagem = "Fornecedor Alterado com sucesso!",
                            IdFornecedor = forn.IdFornecedor
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


        public ActionResult Desativar(int Id)
        {
            try
            {

                using (var cadForn = new CadastroFornecedor())
                {

                    cadForn.DesativarFornecedor(cadForn.GetFornecedorById(Id));

                    return Json(new
                    {
                        TipoMensagem = TipoMensagemRetorno.Ok,
                        Mensagem = "Fornecedor Desativado com sucesso!",
                    }, "text/html", JsonRequestBehavior.AllowGet);

                }
            }
            catch(Exception ex)
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

                using (var cadForn = new CadastroFornecedor())
                {

                    cadForn.ReativarFornecedor(cadForn.GetFornecedorById(Id));

                    return Json(new
                    {
                        TipoMensagem = TipoMensagemRetorno.Ok,
                        Mensagem = "Fornecedor Reativado com sucesso!",
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

        public JsonResult GetContatos([DataSourceRequest] DataSourceRequest request, int IdFornecedor)
        {
            if (IdFornecedor == 0)
                return Json(new List<FornecedorContatoModel>().ToDataSourceResult(request) , JsonRequestBehavior.AllowGet);
            else
            {
                using (var cadForn = new CadastroFornecedor())
                {
                    var contatos = cadForn.GetContatosFornecedor(IdFornecedor);
                    return Json(contatos.Select(x => new FornecedorContatoModel()
                    {
                        Email = x.Email,
                        Nome = x.Nome,
                        Site = x.Site,
                        Telefone = x.Telefone
                    }).ToDataSourceResult(request) , JsonRequestBehavior.AllowGet);
                }
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult AtualizarContatos(IList<FornecedorContatoModel> contatos, int IdFornecedor)
        {
            try
            {


                using (var cadForn = new CadastroFornecedor())
                {

                    cadForn.AtualizarContato((contatos ?? new List<FornecedorContatoModel>()).Select(x => new FornecedorContato()
                    {
                        Email = x.Email,
                        Nome = x.Nome,
                        Site = x.Site,
                        Telefone = x.Telefone
                    }).ToList(), IdFornecedor);

                    return Json(new
                    {
                        TipoMensagem = TipoMensagemRetorno.Ok,
                        Mensagem = "Operação realizada com sucesso.",
                    }, "text/html", JsonRequestBehavior.AllowGet);

                }
            }
            catch(Exception ex)
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