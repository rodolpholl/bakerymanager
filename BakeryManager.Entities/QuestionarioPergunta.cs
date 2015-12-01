using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Entities
{

    public enum TipoResposta
    {
        Eletiva = 1,
        Avaliativa = 2
    }

    public class QuestionarioPergunta
    {
        public virtual int IdQuestionarioPergunta { get; set; }
        public virtual Questionario Questionario { get; set; }
        public virtual string Descricao { get; set; }
        public virtual int Peso { get; set; }
        public virtual TipoResposta TipoResposta { get; set; }

       
    }
}
