using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BakeryManager.BackOffice.Models.Questionario
{

    public class TipoRespostaModel
    {
        public int IdTipoResposta { get; set; }
        public string Descricao { get; set; }
    }

    public class QuestionarioPerguntaModel
    {
        public int IdQuestionarioPergunta { get; set; }
        public string Descricao { get; set; }
        [Range(1,9999,ErrorMessage = "O valor atribuido ao peso deve estar entre 1 e 9999")]
        public int Peso { get; set; }
        [UIHint("TipoResposta")]
        public TipoRespostaModel TipoResposta { get; set; }


      
    }
}