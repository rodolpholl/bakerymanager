using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace BakeryManager.Entities
{
    public class IngredienteFormula
    {
        public virtual int IdIngredienteFormula { get; set; }
        public virtual Formula Formula { get; set; }
        public virtual Ingrediente Ingrediente { get; set; }
        public virtual double Quantidade { get; set; }
        public virtual bool AGosto { get; set; }
    
    }
}
