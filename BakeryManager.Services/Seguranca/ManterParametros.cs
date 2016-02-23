using BakeryManager.Repositories.Seguranca;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BakeryManager.Entities;
using BakeryManager.Repositories;

namespace BakeryManager.Services.Seguranca
{
    public class ManterParametros : BusinessProcessBase, IDisposable
    {
        private ParametrosGeraisBM parametrosGeraisBm;
        private ParametroTabelaNutricionalBM parametroTabelaNutricionalBm;
        private TabelaNutricionalBM tabelaNutricionalBm;
        private CondicaoPagamentoBM condicaoPagamentoBm;
        private DadosBasicosBM dadosBasicosBm;
        private AssuntoMensagemContatoBM assuntoMensagemContatoBm;
        public ManterParametros()
        {
            parametrosGeraisBm = GetObject<ParametrosGeraisBM>();
            parametroTabelaNutricionalBm = GetObject<ParametroTabelaNutricionalBM>();
            tabelaNutricionalBm = GetObject<TabelaNutricionalBM>();
            condicaoPagamentoBm = GetObject<CondicaoPagamentoBM>();
            dadosBasicosBm = GetObject<DadosBasicosBM>();
            assuntoMensagemContatoBm = GetObject<AssuntoMensagemContatoBM>();
        }
        public void Dispose()
        {
            parametrosGeraisBm.Dispose();
            parametroTabelaNutricionalBm.Dispose();
            tabelaNutricionalBm.Dispose();
            condicaoPagamentoBm.Dispose();
            dadosBasicosBm.Dispose();
            assuntoMensagemContatoBm.Dispose();

        }

        public IList<ParametroTabelaNutricional> GetListaTabelaParaExibir()
        {
            return parametroTabelaNutricionalBm.GetAll();
        }

        public IList<TabelaNutricional> GetListaTabelaSemExibicao()
        {
            List<int> listaIdExibicao = GetListaTabelaParaExibir().Select(x => x.Compoonente.IdTabelaNutricional).ToList();

            if (listaIdExibicao == null || listaIdExibicao.Count == 0)
                return tabelaNutricionalBm.GetAll();

            var ListaAExibir = tabelaNutricionalBm.GetAll().Where(x => !listaIdExibicao.Contains(x.IdTabelaNutricional)).ToList();
            return ListaAExibir;
        }

        public void SalvarConfiguracao(List<ParametroTabelaNutricional> list)
        {
            foreach (var old in parametroTabelaNutricionalBm.GetAll())
                parametroTabelaNutricionalBm.Delete(old);

            foreach(var novo in list)
            {
                var parametroTabelaNutricional = new ParametroTabelaNutricional()
                {
                    Compoonente = tabelaNutricionalBm.GetByID(novo.Compoonente.IdTabelaNutricional),
                    Parametros = parametrosGeraisBm.GetAll().FirstOrDefault()
                };

                parametroTabelaNutricionalBm.Insert(parametroTabelaNutricional);
            }
                
            
            
        }

        public DadosBasicos GetDadosBasicos()
        {
            return dadosBasicosBm.GetAll().FirstOrDefault();
        }

        public IList<CondicaoPagamento> GetListaCondicaoPagamento()
        {
            return condicaoPagamentoBm.GetAll();
        }

        public void SalvarCondicaoPagamento(IList<CondicaoPagamento> ListaCondicao)
        {
            foreach (var condicaoAtual in condicaoPagamentoBm.GetAll().Where(x => !ListaCondicao.Select(y => y.IdCondicaoPagamento).ToList().Contains(x.IdCondicaoPagamento)).ToList())
                    condicaoPagamentoBm.Delete(condicaoAtual);




            foreach (var condicao in ListaCondicao)
            {
                if (condicao.IdCondicaoPagamento > 0)
                {
                    var condicaoNova = condicaoPagamentoBm.GetByID(condicao.IdCondicaoPagamento);
                    condicaoNova.Descricao = condicao.Descricao;
                    condicaoPagamentoBm.Update(condicao);
                }
                else
                    condicaoPagamentoBm.Insert(new CondicaoPagamento()
                    {
                        Descricao = condicao.Descricao
                    });
                
            }

            
        }

        public void AtualizarDadosGerais(DadosBasicos pDadosGerais)
        {
            var NovosDados = dadosBasicosBm.GetByID(1);

            if (NovosDados == null)
                NovosDados = new DadosBasicos();


            NovosDados.Alvara = pDadosGerais.Alvara;
            NovosDados.Bairro = pDadosGerais.Bairro;
            NovosDados.CEP = pDadosGerais.CEP;
            NovosDados.Cidade = pDadosGerais.Cidade;
            NovosDados.CNPJ = pDadosGerais.CNPJ;
            NovosDados.Complemento = pDadosGerais.Complemento;
            NovosDados.InscricaoEstadual = pDadosGerais.InscricaoEstadual;
            NovosDados.LatitudeMapa = pDadosGerais.LatitudeMapa;
            NovosDados.Logradouro = pDadosGerais.Logradouro;
            NovosDados.LongitudeMapa = pDadosGerais.LongitudeMapa;
            NovosDados.NomeFantasia = pDadosGerais.NomeFantasia;
            NovosDados.Numero = pDadosGerais.Numero;
            NovosDados.RazaoSocial = pDadosGerais.RazaoSocial;
            NovosDados.UF = pDadosGerais.UF;

            if (NovosDados.IdDadosBasicos == 0)
                dadosBasicosBm.Insert(NovosDados);
            else
                dadosBasicosBm.Update(NovosDados);

        }

        public IList<AssuntoMensagemContato> GetListaAssuntoMensagem()
        {
            return assuntoMensagemContatoBm.GetAll();
        }

        public void SalvarListaAssuntoMensagemContato(List<AssuntoMensagemContato> ListaAssunto)
        {
            foreach (var assuntoAtual in assuntoMensagemContatoBm.GetAll().Where(x => !ListaAssunto.Select(y => y.IdAssuntoMensagemContato).ToList().Contains(x.IdAssuntoMensagemContato)).ToList())
                assuntoMensagemContatoBm.Delete(assuntoAtual);

            foreach (var assunto in ListaAssunto)
            {
                if (assunto.IdAssuntoMensagemContato > 0)
                {
                    var assuntoNovo = assuntoMensagemContatoBm.GetByID(assunto.IdAssuntoMensagemContato);
                    assuntoNovo.Descricao = assunto.Descricao;
                    assuntoMensagemContatoBm.Update(assuntoNovo);
                }
                else
                    assuntoMensagemContatoBm.Insert(new AssuntoMensagemContato()
                    {
                        Descricao = assunto.Descricao
                    });
            }

        }
    }
}
