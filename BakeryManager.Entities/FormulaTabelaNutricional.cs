using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Entities
{
    public class FormulaTabelaNutricional
    {
        public virtual int IdFormulaTabelaNutricional { get; set; }
        public virtual Formula Formula { get; set; }
        public virtual TabelaNutricional Componente { get; set; }
        public virtual double Valor { get; set; }
        public virtual double? PercentualDiario { get; set; }
    }
}
