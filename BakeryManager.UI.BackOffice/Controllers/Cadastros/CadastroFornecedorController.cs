﻿using BakeryManager.BackOffice.Models.Cadastros.Fornecedores;
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
using BakeryManager.BackOffice.Models.Cadastros.Ingredientes;
using BakeryManager.BackOffice.Models.Cadastros;

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

            GetPerfilFornecedor();

            return View(new FornecedorModel()
            {
                Ativo = true,
                IdFornecedor = 0
            });
        }

        private void GetPerfilFornecedor()
        {
            using (var cadForn = new CadastroFornecedor())
            {
                var lista = cadForn.GetPerfilFornecedor().OrderBy(x => x.Nome).Select(x => new PerfilFornecedorUsuarioModel()
                {
                    IdPerfil = x.IdPerfil,
                    Nome = x.Nome
                }).ToList();

                ViewData["ListaPerfilFornecedor"] = lista;
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Criar(FornecedorModel fornecedorModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var cadForn = new CadastroFornecedor())
                    {

                        var forn = new Fornecedor()
                        {
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
                GetPerfilFornecedor();
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
                return Json(new List<FornecedorContatoModel>().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
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
                    }).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
                }
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult AtualizarContatos(IList<FornecedorContatoModel> contatos, IList<FornecedorQuestionarioConfigModel> questionarios,
                                           IList<CredenciamentoFornecedorIngredienteModel> listaCredenciamento, IList<FornecedorUsuarioModel> listaUsuarios,
                                           int IdFornecedor)
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

                    cadForn.AtualizarQuestionario((questionarios ?? new List<FornecedorQuestionarioConfigModel>()).Where(x => x.Selecionado).Select(x => new FornecedorQuestionarioConfig()
                    {
                        Fornecedor = cadForn.GetFornecedorById(IdFornecedor),
                        Questionario = cadForn.GetQuestionarioById(x.IdQuestionario)
                    }).ToList(), IdFornecedor);

                    cadForn.AtualizarCredenciamento((listaCredenciamento ?? new List<CredenciamentoFornecedorIngredienteModel>()).Select(x => new CredenciamentoFornecedorIngrediente()
                    {
                        Fornecedor = cadForn.GetFornecedorById(IdFornecedor),
                        Ingrediente = new Ingrediente()
                        {
                            IdIngrediente = x.IdIngrediente
                        }
                    }).ToList(), IdFornecedor);

                    cadForn.AtualizarUsuario((listaUsuarios ?? new List<FornecedorUsuarioModel>()).Select(x => new FornecedorUsuario()
                    {
                        Fornecedor = cadForn.GetFornecedorById(IdFornecedor),
                        Usuario = new Usuario()
                        {
                            Nome = x.Nome,
                            Login = x.Login,
                            Email = x.Email,
                            IdUsuario = x.IdUsuario
                        },
                        Perfil = new Perfil()
                        {
                            IdPerfil = x.Perfil.IdPerfil,
                            Nome = x.Perfil.Nome
                        },
                        UtilizaEmailComunicacao = x.UtilizaEmailComunicacao

                    }).ToList(), IdFornecedor);

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

        public JsonResult GetQuestionarioFornecedor([DataSourceRequest] DataSourceRequest request, int IdFornecedor)
        {
            using (var cadForn = new CadastroFornecedor())
            {
                var questionarioFornecedor = cadForn.GetQuestionarioFornecedor(cadForn.GetFornecedorById(IdFornecedor)).Select(x => x.Questionario.IdQuestionario).ToList();
                var questionario = cadForn.GetQuestionarioAtivo().Select(x => new FornecedorQuestionarioConfigModel()
                {
                    IdQuestionario = x.IdQuestionario,
                    Questionario = x.Nome,
                    Selecionado = questionarioFornecedor.Contains(x.IdQuestionario)
                }).ToList();

                return Json(questionario.ToTreeDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult ListarCategoriasIngrediente()
        {
            using (var cadForn = new CadastroFornecedor())
            {
                var result = cadForn.GetCategoriaIngredientes();
                return Json(result
                    .Select(x => new CategoriaIngredienteModel()
                    {
                        IdCategoriaIngrediente = x.IdCategoriaIngrediente,
                        Nome = x.Nome

                    }).OrderBy(x => x.Nome).ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GetIngredietesDisponiveis(int? IdCategoria, int[] IngredientesJaSelecionados)
        {



            using (var cadForn = new CadastroFornecedor())
            {


                var result = cadForn.GetCategoriaIngredientesByCategoria(IdCategoria.HasValue ? IdCategoria.Value : 0).AsQueryable();


                if (IngredientesJaSelecionados != null)
                    result = result.Where(x => !IngredientesJaSelecionados.Contains(x.IdIngrediente));

                var res = result.Select(x => new IngredientesModel()
                {
                    IdIngrediente = x.IdIngrediente,
                    Nome = string.IsNullOrWhiteSpace(x.Nome) ? x.NomeTACO : x.Nome
                }).OrderBy(x => x.Nome).ToList();


                return Json(res, JsonRequestBehavior.AllowGet);

            }


        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GetCredenciamentoFornecedor([DataSourceRequest] DataSourceRequest request, int IdFornecedor)
        {
            using (var cadForn = new CadastroFornecedor())
            {
                var result = cadForn.GetCredenciamentoByFornecedor(IdFornecedor).Select(x => new CredenciamentoFornecedorIngredienteModel()
                {
                    IdCredenciamentoFornecedorIngrediente = x.IdCredenciamentoFornecedorIngrediente,
                    IdIngrediente = x.Ingrediente.IdIngrediente,
                    NomeIngrediente = string.IsNullOrWhiteSpace(x.Ingrediente.Nome) ? x.Ingrediente.NomeTACO : x.Ingrediente.Nome
                });

                return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);

            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GetUsuarioFornecedor([DataSourceRequest] DataSourceRequest request, int IdFornecedor)
        {
            using (var cadForn = new CadastroFornecedor())
            {
                var result = cadForn.GetUsuarioFornecedor(IdFornecedor).Select(x => new FornecedorUsuarioModel()
                {
                    Email = IdFornecedor == 0 ? string.Empty : x.Usuario.Email,
                    Nome = IdFornecedor == 0 ? string.Empty : x.Usuario.Nome,
                    IdUsuario = IdFornecedor == 0 ? 0 : x.Usuario.IdUsuario,
                    Login = IdFornecedor == 0 ? string.Empty : x.Usuario.Login,
                    Perfil = new PerfilFornecedorUsuarioModel()
                    {
                        IdPerfil = (IdFornecedor == 0 || x.Perfil == null) ? 0 : x.Perfil.IdPerfil,
                        Nome = (IdFornecedor == 0 || x.Perfil == null) ? string.Empty : x.Perfil.Nome
                    },
                    IdFornecedorUsuario = x.IdFornecedorUsuario,
                    UtilizaEmailComunicacao = x.UtilizaEmailComunicacao,
                    HabilitaEdicao = IdFornecedor == 0 ? true : cadForn.VerificaPossibilidadeEdicao(x)
                });

                return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);

            }
        }


    }
}