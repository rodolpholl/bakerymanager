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
        public ManterParametros()
        {
            parametrosGeraisBm = GetObject<ParametrosGeraisBM>();
            parametroTabelaNutricionalBm = GetObject<ParametroTabelaNutricionalBM>();
            tabelaNutricionalBm = GetObject<TabelaNutricionalBM>();
        }
        public void Dispose()
        {
            parametrosGeraisBm.Dispose();
            parametroTabelaNutricionalBm.Dispose();
            tabelaNutricionalBm.Dispose();

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
                parametroTabelaNutricionalBm.Insert(novo);
            
            
        }
    }
}
