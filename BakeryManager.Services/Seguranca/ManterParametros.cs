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
        public ManterParametros()
        {
            parametrosGeraisBm = GetObject<ParametrosGeraisBM>();
            parametroTabelaNutricionalBm = GetObject<ParametroTabelaNutricionalBM>();
            tabelaNutricionalBm = GetObject<TabelaNutricionalBM>();
            condicaoPagamentoBm = GetObject<CondicaoPagamentoBM>();
        }
        public void Dispose()
        {
            parametrosGeraisBm.Dispose();
            parametroTabelaNutricionalBm.Dispose();
            tabelaNutricionalBm.Dispose();
            condicaoPagamentoBm.Dispose();

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
    }
}
