using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Entities
{
    public class IngredienteEstoqueConfig
    {
        public virtual int IdIngredienteEstoqueConfig { get; set; }
        public virtual Ingrediente Ingrediente { get; set; }
        public virtual double QuantidadePermanentePadrao { get; set; }
        public virtual double PercentualAlerta { get; set; }
    }
}
