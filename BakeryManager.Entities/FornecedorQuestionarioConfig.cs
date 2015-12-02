using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Entities
{
    public class FornecedorQuestionarioConfig
    {
        public virtual int IdFornecedorQuestionarioConfig { get; set; }
        public virtual Questionario Questionario { get; set; }
        public virtual Fornecedor Fornecedor { get; set; }
    }
}
