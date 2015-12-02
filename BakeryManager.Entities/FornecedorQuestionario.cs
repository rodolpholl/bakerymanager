using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Entities
{
    public class FornecedorQuestionario
    {
        public virtual int IdFornecedorQuestionario { get; set; }
        public virtual Fornecedor Fornecedor { get; set; }
        public virtual Questionario Questionario { get; set; }
        public virtual DateTime? DataPreenchimento { get; set; }
        public virtual double MediaObtida { get; set; }
    }
}
