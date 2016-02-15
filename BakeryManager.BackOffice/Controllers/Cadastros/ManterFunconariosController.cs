using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using BakeryManager.Services;
using BakeryManager.BackOffice.Models.Cadastros.Funcionarios;
using BakeryManager.Entities;
using BakeryManager.BackOffice.Models;
using Newtonsoft.Json;

namespace BakeryManager.BackOffice.Controllers.Cadastros
{
    public class ManterFunconariosController : Controller
    {
        // GET: ManterFunconarios
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Read([DataSourceRequest] DataSourceRequest request)
        {
            using (var manterFuncionario = new ManterFuncionarios())
                return Json(manterFuncionario.GetFuncionariosAll().OrderBy(x => x.Nome)
                    .Select(x => new FuncionarioModel()
                    {
                         
                        Bairro = x.Bairro,
                        CEP = x.CEP,
                        Cidade = x.Cidade,
                        Complemento = x.Complemento,
                        CPF = x.CPF,
                        CTPS = x.CTPS,
                        DataInicioTrabalho = x.DataInicioTrabalho,
                        Email = x.Email,
                        HorarioEntrada = x.HorarioEntrada,
                        HorarioSaida = x.HorarioSaida,
                        IdFuncionario = x.IdFuncionario,
                        Logradouro = x.Logradouro,
                        Nome = x.Nome,
                        Numero = x.Numero,
                        RemuneracaoAtual = x.RemuneracaoAtual,
                        RG = x.RG,
                        TelefoneCelular = x.TelefoneCelular,
                        TelefoneFixo = x.TelefoneFixo,
                        UF = x.UF,
                        SituacaoAtual = new SituacaoFucionarioModel()
                        {
                            Descricao = Enum.GetName(typeof(SituacaoFuncionario), x.SituacaoAtual),
                            IdSituacaoFuncionario = (int)x.SituacaoAtual
                        }                        

                    }).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);

        }

        public ActionResult Criar()
        {

            setViewData();
            return View(new FuncionarioModel());
        }

        private void setViewData()
        {
            using (var manterFuncionario = new ManterFuncionarios())
            {
                ViewData["ListaPerfil"] = manterFuncionario.GetListaPerfil().Select(x => new SelectListItem()
                {
                    Value = x.IdPerfil.ToString(),
                    Text = x.Nome
                }).ToList();

                
            }
        }
        

        [HttpPost]
        public JsonResult Criar(string strFuncionario)
        {

            var pFuncionario = JsonConvert.DeserializeObject<FuncionarioModel>(strFuncionario);

            try
            {
                using (var manterFuncionario = new ManterFuncionarios())
                {
                    var funcionario = new Funcionario()
                    {
                        Bairro = pFuncionario.Bairro.ToUpper(),
                        CEP = pFuncionario.CEP,
                        Cidade = pFuncionario.Cidade.ToUpper(),
                        Complemento = pFuncionario.Complemento.ToUpper(),
                        CPF = pFuncionario.CPF,
                        CTPS = pFuncionario.CTPS,
                        DataInicioTrabalho = pFuncionario.DataInicioTrabalho,
                        Email = pFuncionario.Email,
                        HorarioEntrada = pFuncionario.HorarioEntrada,
                        HorarioSaida = pFuncionario.HorarioSaida,
                        IdFuncionario = pFuncionario.IdFuncionario,
                        Logradouro = pFuncionario.Logradouro.ToUpper(),
                        Nome = pFuncionario.Nome.ToUpper(),
                        Numero = pFuncionario.Numero,
                        RemuneracaoAtual = pFuncionario.RemuneracaoAtual,
                        RG = pFuncionario.RG,
                        TelefoneCelular = pFuncionario.TelefoneCelular,
                        TelefoneFixo = pFuncionario.TelefoneFixo,
                        UF = pFuncionario.UF.ToUpper(),
                        SituacaoAtual = (SituacaoFuncionario)Enum.Parse(typeof(SituacaoFuncionario), pFuncionario.SituacaoAtual.IdSituacaoFuncionario.ToString())
                    };

                    manterFuncionario.InserirFuncionario(funcionario);

                    AtualizarUsuarioFuncionario(funcionario, pFuncionario);

                    return Json(new
                    {
                        TipoMensagem = TipoMensagemRetorno.Ok,
                        Mensagem = "Funcionário Inserido com sucesso!",
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

        private void AtualizarUsuarioFuncionario(Funcionario Funcionario, FuncionarioModel FuncionarioModel)
        {
           
            using (var manterFuncionario = new ManterFuncionarios())
            {
                if (FuncionarioModel.PossuiAcessoSistema)
                {
                    manterFuncionario.AtualizarFuncionario(Funcionario, FuncionarioModel.Login, FuncionarioModel.UsaSenhaDia,FuncionarioModel.IdPefil);
                }
                else
                {
                    manterFuncionario.DesativarUsuario(Funcionario);
                }
            }
            
        }

        public JsonResult GetListaSituacaoFuncionario()
        {
            var listaRetorno = Enum.GetNames(typeof(SituacaoFuncionario)).Select(x => new SituacaoFucionarioModel()
            {
                Descricao = x,
                IdSituacaoFuncionario = (int)Enum.Parse(typeof(SituacaoFuncionario), x)
            }).ToList();

            return Json( listaRetorno,JsonRequestBehavior.AllowGet);
        }


        public ActionResult Editar(int Id)
        {
           

            using (var manterFuncionario = new ManterFuncionarios())
            {
                var funcionario = manterFuncionario.GetFuncionarioById(Id);
                var funcionarioModel = new FuncionarioModel()
                {
                    Bairro = funcionario.Bairro,
                    CEP = funcionario.CEP,
                    Cidade = funcionario.Cidade,
                    Complemento = funcionario.Complemento,
                    CPF = funcionario.CPF,
                    CTPS = funcionario.CTPS,
                    DataInicioTrabalho = funcionario.DataInicioTrabalho,
                    Email = funcionario.Email,
                    HorarioEntrada = funcionario.HorarioEntrada,
                    HorarioSaida = funcionario.HorarioSaida,
                    IdFuncionario = funcionario.IdFuncionario,
                    Logradouro = funcionario.Logradouro,
                    Nome = funcionario.Nome,
                    Numero = funcionario.Numero,
                    RemuneracaoAtual = funcionario.RemuneracaoAtual,
                    RG = funcionario.RG,
                    TelefoneCelular = funcionario.TelefoneCelular,
                    TelefoneFixo = funcionario.TelefoneFixo,
                    UF = funcionario.UF,
                    SituacaoAtual = new SituacaoFucionarioModel()
                    {
                        Descricao = Enum.GetName(typeof(SituacaoFuncionario), funcionario.SituacaoAtual),
                        IdSituacaoFuncionario = (int)funcionario.SituacaoAtual
                    },
                    PossuiAcessoSistema = manterFuncionario.GetUsuarioPorFuncionario(funcionario) != null,
                    Login = manterFuncionario.GetUsuarioPorFuncionario(funcionario) != null ? manterFuncionario.GetUsuarioPorFuncionario(funcionario).Login : string.Empty,
                    UsaSenhaDia = manterFuncionario.GetUsuarioPorFuncionario(funcionario) != null ? manterFuncionario.GetUsuarioPorFuncionario(funcionario).AutenticaSenhaDia : false,
                    IdPefil = manterFuncionario.GetUsuarioPorFuncionario(funcionario) != null ? manterFuncionario.GetPerfilByUsuario(manterFuncionario.GetUsuarioPorFuncionario(funcionario)).IdPerfil : 2
                };


                setViewData();

                return View(funcionarioModel);
            }
        }

        [HttpPost]
        public JsonResult Editar(string strFuncionario)
        {

            var pFuncionario = JsonConvert.DeserializeObject<FuncionarioModel>(strFuncionario);

            try
            {
                using (var manterFuncionario = new ManterFuncionarios())
                {
                    var funcionario = manterFuncionario.GetFuncionarioById(pFuncionario.IdFuncionario);

                    funcionario.Bairro = pFuncionario.Bairro.ToUpper();
                    funcionario.CEP = pFuncionario.CEP;
                    funcionario.Cidade = pFuncionario.Cidade.ToUpper();
                    funcionario.Complemento = pFuncionario.Complemento.ToUpper();
                    funcionario.CPF = pFuncionario.CPF;
                    funcionario.CTPS = pFuncionario.CTPS;
                    funcionario.DataInicioTrabalho = pFuncionario.DataInicioTrabalho;
                    funcionario.Email = pFuncionario.Email;
                    funcionario.HorarioEntrada = pFuncionario.HorarioEntrada;
                    funcionario.HorarioSaida = pFuncionario.HorarioSaida;
                    funcionario.IdFuncionario = pFuncionario.IdFuncionario;
                    funcionario.Logradouro = pFuncionario.Logradouro.ToUpper();
                    funcionario.Nome = pFuncionario.Nome.ToUpper();
                    funcionario.Numero = pFuncionario.Numero;
                    funcionario.RemuneracaoAtual = pFuncionario.RemuneracaoAtual;
                    funcionario.RG = pFuncionario.RG;
                    funcionario.TelefoneCelular = pFuncionario.TelefoneCelular;
                    funcionario.TelefoneFixo = pFuncionario.TelefoneFixo;
                    funcionario.UF = pFuncionario.UF.ToUpper();
                    funcionario.SituacaoAtual = (SituacaoFuncionario)Enum.Parse(typeof(SituacaoFuncionario), pFuncionario.SituacaoAtual.IdSituacaoFuncionario.ToString());
                   
                    manterFuncionario.AlterarFuncionario(funcionario);

                    AtualizarUsuarioFuncionario(funcionario, pFuncionario);

                    return Json(new
                    {
                        TipoMensagem = TipoMensagemRetorno.Ok,
                        Mensagem = "Funcionário Alterado com sucesso!",
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