using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Entities
{
    public class IngredienteTabelaNutricional
    {
        public virtual int IdIngredienteTabelaNutricional { get; set; }
        public virtual Ingrediente Ingrediente { get; set; }
        public virtual TabelaNutricional Componente { get; set; }
        public virtual double Valor { get; set; }
        
    }
}
