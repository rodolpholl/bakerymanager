using BakeryManager.BackOffice.Models;
using BakeryManager.BackOffice.Models.Cadastros.Fornecedores;
using BakeryManager.Entities;
using BakeryManager.InfraEstrutura.Helpers;
using BakeryManager.Services;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace BakeryManager.BackOffice.Controllers
{
    [Authorize]
    public class AvaliacaoFornecedorController : Controller
    {
        // GET: AvaliacaoFornecedor
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult GetListaFornecedor()
        {
            using (var avaliacaoForn = new AvaliacaoFornecedor())
            {
                return Json(avaliacaoForn.GetListaFornecedorComQuestionarioAssociado().Select(x => new
                {
                    Nome = x.Nome,
                    IdFornecedor = x.IdFornecedor
                }).ToList(), JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult Read([DataSourceRequest] DataSourceRequest request, int? IdFornecedor)
        {
            if (IdFornecedor.HasValue)
            {
                using (var avaliacaoForn = new AvaliacaoFornecedor())
                {
                    var listaAvaliacao = avaliacaoForn.GetListaAvaliacaoByFornecedor(IdFornecedor.Value).Select(x => new FornecedorAvaliacaoModel()
                    {
                        IdFornecedorAvaliacao = x.IdFornecedorAvaliacao,
                        DataAlteracao = x.DataAlteracao,
                        DataCriacao = x.DataCriacao,
                        UsuarioAteracao = x.UsuarioAteracao,
                        IpAlteracao = x.IpAlteracao,
                        MediaAvaliacao = Math.Round(x.MediaObtida,2),
                        HabilitaEdicao = avaliacaoForn.HabilitaEdicaoAvaliacao(x.IdFornecedorAvaliacao)
                    }).ToList();

                    return Json(listaAvaliacao.ToTreeDataSourceResult(request), JsonRequestBehavior.AllowGet);
                }
            }

            else
                return Json(new List<FornecedorAvaliacaoModel>().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult GetQuestionarioFornecedor(int IdFornecedor)
        {
            using (var avaliacaoForn = new AvaliacaoFornecedor())
            {

                var listaQuestionarioFornecedor = avaliacaoForn.GetQuestionarioFornecedor(IdFornecedor).OrderBy(x => x.Pergunta.Questionario.IdQuestionario);
                var listaQuestinario = listaQuestionarioFornecedor.Select(x => x.Pergunta.Questionario).Distinct()
                                       .Select(x => new FornecedorAvaliacaoQuestionarioModel()
                                       {
                                           IdQuestionario = x.IdQuestionario,
                                           Nome = x.Nome,
                                           Respostas =
                                               listaQuestionarioFornecedor.Where(y => y.Pergunta.Questionario.IdQuestionario == x.IdQuestionario)
                                               .Select(y => new FornecedorAvaliacaoRespostaModel()
                                               {
                                                   Nota = y.Avaliacao,
                                                   IdPergunta = y.Pergunta.IdQuestionarioPergunta,
                                                   TextoPergunta = y.Pergunta.Descricao,
                                                   OpcaoEletiva = y.Verdadeiro,
                                                   TipoResposta = (TipoRespostaModel)((int)y.Pergunta.TipoResposta),
                                                   IdForeceIdFornecedorQuestionarioResposta = y.IdFornecedorAvaliacaoQuestionarioResposta
                                               })
                                               .ToList()


                                       }).ToList();

                return Json(MVCHelper.RenderRazorViewToString(this, Url.Content("~/Views/AvaliacaoFornecedor/TabelaAutomatica.cshtml"), listaQuestinario), JsonRequestBehavior.AllowGet);


            }
        }

        public ActionResult Criar(int IdFornecedor)
        {
            using (var avaliacaoForn = new AvaliacaoFornecedor())
            {
                return View(new FornecedorAvaliacaoModel()
                {
                    IdFornecedor = IdFornecedor,
                    ListaQuestionarios = avaliacaoForn.GetQuestionarioConfigByFornecedor(IdFornecedor).Select(x => new FornecedorAvaliacaoQuestionarioModel()
                    {
                        IdFornecedorAvaliacaoQuestionario = 0,
                        IdQuestionario = x.Questionario.IdQuestionario,
                        Nome = x.Questionario.Nome,
                        Respostas = avaliacaoForn.GetPerguntasByQuestionario(x.Questionario.IdQuestionario).Select(y => new FornecedorAvaliacaoRespostaModel()
                        {
                            IdPergunta = y.IdQuestionarioPergunta,
                            TextoPergunta = y.Descricao,
                            TipoResposta = (TipoRespostaModel)y.TipoResposta
                        }).ToList()
                    }).ToList()
                });
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Criar(string strFornecedorAvaliacao)
        {
            try {
                var FornecedorAvaliacao = JsonConvert.DeserializeObject<FornecedorAvaliacaoModel>(strFornecedorAvaliacao);

                using (var avaliacaoForn = new AvaliacaoFornecedor())
                {
                    var avaliacao = new FornecedorAvaliacao()
                    {
                        Fornecedor = avaliacaoForn.GetFornecedorById(FornecedorAvaliacao.IdFornecedor),
                        IpAlteracao = Request.ServerVariables["REMOTE_ADDR"],
                        UsuarioAteracao = User.Identity.Name,
                        Observacao = FornecedorAvaliacao.Observacao
                    };

                    avaliacaoForn.InserirAvaliacao(avaliacao);

                    AtualizarQuestionario(avaliacao, FornecedorAvaliacao.ListaQuestionarios);

                    return Json(new
                    {
                        TipoMensagem = TipoMensagemRetorno.Ok,
                        Mensagem = "Avaliação criada com sucesso!"
                    }, "text/html", JsonRequestBehavior.AllowGet);
                }
            }
            catch(Exception ex)
            {
                return Json(new
                {
                    TipoMensagem = TipoMensagemRetorno.Erro,
                    Mensagem = ex.Message
                }, "text/html", JsonRequestBehavior.AllowGet);
            }
        }

        private void AtualizarQuestionario(FornecedorAvaliacao avaliacao, IList<FornecedorAvaliacaoQuestionarioModel> listaQuestionarios)
        {
            using (var avaliacaoForn = new AvaliacaoFornecedor())
            {
                foreach (var questionario in listaQuestionarios)
                {
                    var fornecedorQuestionario = new FornecedorAvaliacaoQuestionario()
                    {
                        Avaliacao = avaliacao,
                        IdFornecedorAvaliacaoQuestionario = questionario.IdFornecedorAvaliacaoQuestionario,
                        Questionario = avaliacaoForn.getQuestionarioById(questionario.IdQuestionario)
                    };

                    var listarResposta = listaQuestionarios.FirstOrDefault(x => x.IdQuestionario == questionario.IdQuestionario).Respostas;

                    var listaFornecedorQuestionarioResposta = listarResposta.Select(y => new FornecedorAvaliacaoQuestionarioResposta()
                    {
                        Avaliacao = y.Nota,
                        FornecedorQuestionario = fornecedorQuestionario,
                        Pergunta = new QuestionarioPergunta() { IdQuestionarioPergunta = y.IdPergunta },
                        Verdadeiro = y.OpcaoEletiva.HasValue ? y.OpcaoEletiva.Value : false
                    }).ToList();


                    avaliacaoForn.AtualizarQuestionario(avaliacao,fornecedorQuestionario,listaFornecedorQuestionarioResposta);
                    
                }

                avaliacaoForn.AtualizarMediaAvaliacao(avaliacao);
            }
        }

        public ActionResult Editar(int Id)
        {
            using (var avaliacaoForn = new AvaliacaoFornecedor())
            {
                var avaliacao = avaliacaoForn.getAvaliacaoById(Id);


                var avaliacaoModel = new FornecedorAvaliacaoModel()
                {
                    DataAlteracao = avaliacao.DataAlteracao,
                    DataCriacao = avaliacao.DataCriacao,
                    IdFornecedor = avaliacao.Fornecedor.IdFornecedor,
                    IdFornecedorAvaliacao = avaliacao.IdFornecedorAvaliacao,
                    IpAlteracao = avaliacao.IpAlteracao,
                    Observacao = avaliacao.Observacao,
                    UsuarioAteracao = avaliacao.UsuarioAteracao,
                    ListaQuestionarios = new List<FornecedorAvaliacaoQuestionarioModel>()
                   
                };

                var listaQuestionarioAtual = avaliacaoForn.GetFornecedorAvaliacaoQuestionarioByAvaliacao(avaliacao).Select(x => new FornecedorAvaliacaoQuestionarioModel()
                {
                    IdFornecedorAvaliacaoQuestionario = x.IdFornecedorAvaliacaoQuestionario,
                    IdQuestionario = x.Questionario.IdQuestionario,
                    Nome = x.Questionario.Nome,
                    Respostas = avaliacaoForn.GetFornecedorAvaliacaoQuestionarioRespostaByAvaliacaoQuestionario(x).Select(y => new FornecedorAvaliacaoRespostaModel()
                    {
                        IdPergunta = y.Pergunta.IdQuestionarioPergunta,
                        TextoPergunta = y.Pergunta.Descricao,
                        TipoResposta = (TipoRespostaModel)y.Pergunta.TipoResposta,
                        Nota = y.Avaliacao,
                        OpcaoEletiva = y.Verdadeiro
                    }).ToList()
                }).ToList();

               

                avaliacaoModel.ListaQuestionarios = listaQuestionarioAtual;

                return View(avaliacaoModel);
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Editar(string strFornecedorAvaliacao)
        {
            try
            {
                var FornecedorAvaliacao = JsonConvert.DeserializeObject<FornecedorAvaliacaoModel>(strFornecedorAvaliacao);

                using (var avaliacaoForn = new AvaliacaoFornecedor())
                {
                    var avaliacao = avaliacaoForn.getAvaliacaoById(FornecedorAvaliacao.IdFornecedorAvaliacao);


                    avaliacao.IpAlteracao = Request.ServerVariables["REMOTE_ADDR"];
                    avaliacao.UsuarioAteracao = User.Identity.Name;
                    avaliacao.Observacao = FornecedorAvaliacao.Observacao;
                    

                    avaliacaoForn.AlterarAvaliacao(avaliacao);

                    AtualizarQuestionario(avaliacao, FornecedorAvaliacao.ListaQuestionarios);

                    return Json(new
                    {
                        TipoMensagem = TipoMensagemRetorno.Ok,
                        Mensagem = "Avaliação Alterada com sucesso!"
                    }, "text/html", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    TipoMensagem = TipoMensagemRetorno.Erro,
                    Mensagem = ex.Message
                }, "text/html", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult EnviarQuestionario(IList<FornecedorAvaliacaoQuestionarioModel> ListaQuestionario, int IdFornecedor, string Observacao)
        {
            try
            {
                using (var avaliacaoForn = new AvaliacaoFornecedor())
                {




                    return Json(new
                    {
                        TipoMensagem = TipoMensagemRetorno.Ok,
                        Mensagem = "Questionário Enviado com Sucesso!!"
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