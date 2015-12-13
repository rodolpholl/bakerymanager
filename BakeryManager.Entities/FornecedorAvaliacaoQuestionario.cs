using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Entities
{
    public class FornecedorAvaliacaoQuestionario
    {
        public virtual int IdFornecedorAvaliacaoQuestionario { get; set; }
        public virtual FornecedorAvaliacao Avaliacao { get; set; }
        public virtual Questionario Questionario { get; set; }
        public virtual DateTime? DataPreenchimento { get; set; }
        public virtual double MediaObtida { get; set; }
     
    }
}
