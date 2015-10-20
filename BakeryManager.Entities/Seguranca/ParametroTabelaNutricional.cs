using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Entities.Seguranca
{
    public class ParametroTabelaNutricional
    {
        public virtual int idParametroTabelaNutricional { get; set; }
        public virtual ParametrosGerais Parametros { get; set; }
        public virtual TabelaNutricional Compoonente { get; set; }
    }
}
