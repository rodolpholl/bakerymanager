using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Entities
{
    public class FornecedorContato
    {
        public virtual int IdFornecedorContato { get; set; }
        public virtual Fornecedor Fornecedor { get; set; }
        public virtual string Nome { get; set; }
        public virtual string Telefone { get; set; }
        public virtual string Email { get; set; }
        public virtual string Site { get; set; }
    }
}
