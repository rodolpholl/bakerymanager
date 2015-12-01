using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using BakeryManager.BackOffice.Models.Questionario;
using BakeryManager.Entities;
using BakeryManager.Services;
using BakeryManager.BackOffice.Models;
using Newtonsoft.Json;

namespace BakeryManager.BackOffice.Controllers
{
    [Authorize]
    public class ManterQuestionarioController : Controller
    {
        // GET: ManterQuestionario
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetListaTipoResposta()
        {
            List<TipoRespostaModel> enumList = GetTipoRespostas();

            return Json(enumList, JsonRequestBehavior.AllowGet);

        }

        private static List<TipoRespostaModel> GetTipoRespostas()
        {
            return Enum.GetNames(typeof(TipoResposta)).Select(x => new TipoRespostaModel()
            {
                Descricao = x,
                IdTipoResposta = (int)Enum.Parse(typeof(TipoResposta), x)
            }).ToList();
        }

        public JsonResult Read([DataSourceRequest] DataSourceRequest request)
        {
            using (var manterQuestionario = new ManterQuestionario())
            {
                return Json(manterQuestionario.GetListaQuestionario().Select(x => new QuestionarioModel()
                {
                    Ativo = x.Ativo,
                    DataCriacao = x.DataCriacao,
                    DataExpiracao = x.DataExpiracao,
                    IdQuestionario = x.IdQuestionario,
                    Nome = x.Nome,
                    PrazoExpiracao = x.PrazoExpiracao,
                    UsaPrazoExpiracao = x.UsaPrazoExpiracao
                }).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Criar()
        {

            ViewData["ListaTipoResposta"] = GetTipoRespostas().AsEnumerable();
            return View(new QuestionarioModel());
        }

        [HttpPost]
        public JsonResult Criar(QuestionarioModel questionarioModel)
        {
            try
            {

                
                using (var manterQuestionario = new ManterQuestionario())
                {
                    var questionario = new Questionario()
                    {
                        Ativo = true,
                        Nome = questionarioModel.Nome.ToUpper(),
                        DataCriacao = DateTime.Now,
                        PrazoExpiracao = questionarioModel.PrazoExpiracao,
                        UsaPrazoExpiracao = questionarioModel.UsaPrazoExpiracao
                    };

                    manterQuestionario.InserirQuestionario(questionario);
                    return Json(new
                    {
                        TipoMensagem = TipoMensagemRetorno.Ok,
                        Mensagem = "Fornecedor Inserido com sucesso!",
                        IdQuestionario = questionario.IdQuestionario,
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

        public ActionResult Editar(int Id)
        {
            ViewData["ListaTipoResposta"] = GetTipoRespostas().AsEnumerable();
            using (var manterQuestionario = new ManterQuestionario())
            {

                var questionario = manterQuestionario.GetQuestionarioById(Id);

                return View(new QuestionarioModel()
                {
                    IdQuestionario = questionario.IdQuestionario,
                    Nome = questionario.Nome,
                    UsaPrazoExpiracao = questionario.UsaPrazoExpiracao,
                    PrazoExpiracao = questionario.PrazoExpiracao
                });
            }
        }
        [HttpPost]
        public JsonResult Editar(QuestionarioModel questionarioModel)
        {
            try
            {
                
                using (var manterQuestionario = new ManterQuestionario())
                {

                    var questionario = manterQuestionario.GetQuestionarioById(questionarioModel.IdQuestionario);

                    questionario.Nome = questionarioModel.Nome.ToUpper();
                    questionario.PrazoExpiracao = questionarioModel.PrazoExpiracao;
                    questionario.UsaPrazoExpiracao = questionarioModel.UsaPrazoExpiracao;
                    

                    manterQuestionario.AlterarQuestionario(questionario);
                    return Json(new
                    {
                        TipoMensagem = TipoMensagemRetorno.Ok,
                        Mensagem = "Fornecedor Inserido com sucesso!",
                        IdQuestionario = questionario.IdQuestionario,
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

        public JsonResult GetPerguntasQuestionario([DataSourceRequest] DataSourceRequest request, int IdQuestionario)
        {
            using (var manterQuestionario = new ManterQuestionario())
            {

                var listaPerguntas = manterQuestionario.GetPerguntasQuestionario(manterQuestionario.GetQuestionarioById(IdQuestionario));

                return Json(listaPerguntas.Select(x => new QuestionarioPerguntaModel()
                {
                    Descricao = x.Descricao,
                    IdQuestionarioPergunta = x.IdQuestionarioPergunta,
                    Peso = x.Peso,
                    TipoResposta = new TipoRespostaModel()
                    {
                        Descricao = Enum.GetName(typeof(TipoResposta), x.TipoResposta),
                        IdTipoResposta = (int)x.TipoResposta
                    }
                   
                   
                }).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult AtualizarPergunta(string strListaPerguntaModel, int IdQuestionario)
        {
            try
            {

                var ListaPerguntaModel = JsonConvert.DeserializeObject<IList<QuestionarioPerguntaModel>>(strListaPerguntaModel);

                using (var manterQuestionario = new ManterQuestionario())
                {
                    manterQuestionario.AtualizarContatos((ListaPerguntaModel ?? new List<QuestionarioPerguntaModel>()).Select(x => new QuestionarioPergunta()
                    {
                        Descricao = x.Descricao,
                        Peso = x.Peso,
                        Questionario = manterQuestionario.GetQuestionarioById(IdQuestionario),
                        TipoResposta = (TipoResposta)x.TipoResposta.IdTipoResposta
                    }), IdQuestionario);

                }

                return Json(new
                {
                    TipoMensagem = TipoMensagemRetorno.Ok,
                    Mensagem = "Operação realizada com sucesso.",
                }, "text/html", JsonRequestBehavior.AllowGet);

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

                using (var manterQuestionario = new ManterQuestionario())
                {

                    manterQuestionario.Desativar(manterQuestionario.GetQuestionarioById(Id));

                    return Json(new
                    {
                        TipoMensagem = TipoMensagemRetorno.Ok,
                        Mensagem = "Questionário Desativado com sucesso!",
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

                using (var manterQuestionario = new ManterQuestionario())
                {

                    manterQuestionario.Reativar(manterQuestionario.GetQuestionarioById(Id));

                    return Json(new
                    {
                        TipoMensagem = TipoMensagemRetorno.Ok,
                        Mensagem = "Questionário Reativado com sucesso!",
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