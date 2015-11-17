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
                        IdFornecedor = x.IdFornecedor
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
            try {
                using (var cadForn = new CadastroFornecedor())
                {

                    var forn = new Fornecedor()
                    {
                        Ativo = fornecedorModel.Ativo,
                        Bairro = fornecedorModel.Bairro,
                        CEP = fornecedorModel.CEP,
                        Cidade = fornecedorModel.Cidade,
                        CNPJ = fornecedorModel.CNPJ,
                        Complemento = fornecedorModel.Complemento,
                        Logradouro = fornecedorModel.Logradouro,
                        Nome = fornecedorModel.Nome,
                        Numero = fornecedorModel.Numero,
                        RazaoSocial = fornecedorModel.RazaoSocial,
                        UF = fornecedorModel.UF
                    };

                    cadForn.InserirFornecedor(forn);


                    return Json(new
                    {
                        TipoMensagem = TipoMensagemRetorno.Ok,
                        Mensagem = "Fornecedor Inserido com sucesso!",
                        URLDestino = Url.Action("Criar"),
                        IdProduto = forn.IdFornecedor
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

        public ActionResult Editar(int Id)
        {
            using (var cadForn = new CadastroFornecedor())
            {

                var forn = cadForn.GetFornecedorById(Id);

                return View(new FornecedorModel()
                {
                    Ativo = forn.Ativo,
                    Bairro = forn.Bairro,
                    CEP = forn.CEP,
                    Cidade = forn.Cidade,
                    CNPJ = forn.CNPJ,
                    Complemento = forn.Complemento,
                    Logradouro = forn.Logradouro,
                    Nome = forn.Nome,
                    Numero = forn.Numero,
                    RazaoSocial = forn.RazaoSocial,
                    UF = forn.UF
                });


            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Editar(FornecedorModel fornecedorModel)
        {
            try
            {
                using (var cadForn = new CadastroFornecedor())
                {
                    
                    var forn = cadForn.GetFornecedorById(fornecedorModel.IdFornecedor);
                    
                    forn.Ativo = fornecedorModel.Ativo;
                    forn.Bairro = fornecedorModel.Bairro;
                    forn.CEP = fornecedorModel.CEP;
                    forn.Cidade = fornecedorModel.Cidade;
                    forn.CNPJ = fornecedorModel.CNPJ;
                    forn.Complemento = fornecedorModel.Complemento;
                    forn.Logradouro = fornecedorModel.Logradouro;
                    forn.Nome = fornecedorModel.Nome;
                    forn.Numero = fornecedorModel.Numero;
                    forn.RazaoSocial = fornecedorModel.RazaoSocial;
                    forn.UF = fornecedorModel.UF;
                    
                    cadForn.AlterarFornecedor(forn);
                    
                    return Json(new
                    {
                        TipoMensagem = TipoMensagemRetorno.Ok,
                        Mensagem = "Fornecedor Alterado com sucesso!",
                        URLDestino = Url.Action("Index"),
                        IdProduto = forn.IdFornecedor
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

    }
}