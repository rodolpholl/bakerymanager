using BakeryManager.BackOffice.Models;
using BakeryManager.BackOffice.Models.Cadastros.Fornecedores;
using BakeryManager.InfraEstrutura.Helpers;
using BakeryManager.Services;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            return View(new FornecedorAvaliacaoModel()
            {
                IdFornecedor = IdFornecedor
            });
        }

        public ActionResult Editar(int IdFornecedorAvaliacao)
        {
            using (var avaliacaoForn = new AvaliacaoFornecedor())
            {
                var avaliacao = avaliacaoForn.getAvaliacaoById(IdFornecedorAvaliacao);

                return View(new FornecedorAvaliacaoModel()
                {
                    DataAlteracao = avaliacao.DataAlteracao,
                    DataCriacao = avaliacao.DataCriacao,
                    IdFornecedor = avaliacao.Fornecedor.IdFornecedor,
                    IdFornecedorAvaliacao = avaliacao.IdFornecedorAvaliacao,
                    IpAlteracao = avaliacao.IpAlteracao,
                    Observacao = avaliacao.Observacao,
                    UsuarioAteracao = avaliacao.UsuarioAteracao
                });
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