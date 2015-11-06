using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Entities
{
    public class Fornecedor
    {
        public virtual int IdFornecedor { get; set; }
        public virtual string Nome { get; set; }
        public virtual string RazaoSocial { get; set; }
    }
}
