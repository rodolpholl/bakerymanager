using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Entities
{
    public class Estoque
    {
        public virtual int IdEstoque { get; set; }
        public virtual Ingrediente Ingrediete { get; set; }
        public virtual double QuantidadeAtual { get; set; }
    }
}
