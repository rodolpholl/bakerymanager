using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Entities
{
    public class FornecedorAvaliacaoQuestionarioResposta
    {
        public virtual int IdFornecedorAvaliacaoQuestionarioResposta { get; set; }
        public virtual FornecedorAvaliacaoQuestionario FornecedorQuestionario { get; set; }
        public virtual QuestionarioPergunta Pergunta { get; set; }
        public virtual double? Avaliacao { get; set; }
        public virtual bool Verdadeiro { get; set; }

    }
}
