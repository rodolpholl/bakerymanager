using BakeryManager.Entities;
using BakeryManager.Services.WebsiteServices;
using BakeryManager.UI.WebsiteServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BakeryManager.UI.WebsiteServices.Controllers
{
    public class ContatoController : ApiController
    {
        [HttpGet]
        public IEnumerable<AssuntoMensagemContatoModel> GetListaAssuntoMensagemContato()
        {
            using (var mensagemContato = new EnviarMensagemContato())
            {
                return mensagemContato.GetListaAssuntos().Select(x => new AssuntoMensagemContatoModel()
                {
                    Descricao = x.Descricao,
                    IdAssuntoMensagemContato = x.IdAssuntoMensagemContato
                }).ToList();
            }
        }

        [HttpGet]
        public DadosBasicosModel GetCoordenadasEmpresa()
        {
            using (var mensagemContato = new EnviarMensagemContato())
            {
                var retorno = mensagemContato.GetDadosContatoEmpresa();

                return new DadosBasicosModel()
                {
                    LatitudeMapa = retorno.LatitudeMapa,
                    LongitudeMapa  =retorno.LongitudeMapa
                };
            }
        }

        [HttpPost]
        public void InserirMensagemContato(MensgemContatoModel Mensagem)
        {
            using (var mensagemContato = new EnviarMensagemContato())
            {
                mensagemContato.InserirMensagem(new MensagemContato()
                {
                    Assunto = mensagemContato.GetAssuntoById(Mensagem.Assunto.IdAssuntoMensagemContato),
                    Conteudo = Mensagem.Conteudo,
                    DataEnvioMensagem = DateTime.Now,
                    Email = Mensagem.Email,
                    Nome = Mensagem.Nome,
                    Telefone = Mensagem.Telefone
                });
            }
        }
    }
}
