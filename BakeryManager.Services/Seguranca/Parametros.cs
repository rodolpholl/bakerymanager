using BakeryManager.Repositories.Seguranca;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Services.Seguranca
{
    public class Parametros : BusinessProcessBase, IDisposable
    {
        private ParametrosGeraisBM parametrosGeraisBM;
        public Parametros()
        {
            parametrosGeraisBM = GetObject<ParametrosGeraisBM>();
        }
        public void Dispose()
        {
            parametrosGeraisBM.Dispose();
        }
    }
}
