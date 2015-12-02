using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Entities
{
    public class FornecedorQuestionarioResposta
    {
        public virtual int IdFornecedorQuestionarioResposta { get; set; }
        public virtual FornecedorQuestionario FornecedorQuestionario { get; set; }
        public virtual QuestionarioPergunta Pergunta { get; set; }
        public virtual decimal Avaliacao { get; set; }
        public virtual bool Verdadeiro { get; set; }

    }
}
