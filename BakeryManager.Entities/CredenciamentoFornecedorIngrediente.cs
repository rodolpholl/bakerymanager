using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Entities
{
    public class CredenciamentoFornecedorIngrediente
    {
        public virtual int IdCredenciamentoFornecedorIngrediente { get; set; }
        public virtual Fornecedor Fornecedor { get; set; }
        public virtual Ingrediente Ingrediente { get; set; }
    }
}
