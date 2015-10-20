using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Entities
{
    public class ParametroTabelaNutricional
    {
        public virtual int IdParametroTabelaNutricional { get; set; }
        public virtual ParametrosGerais Parametros { get; set; }
        public virtual TabelaNutricional Compoonente { get; set; }
        
    }
}
