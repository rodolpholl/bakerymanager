using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BakeryManager.BackOffice.Models.Cadastros.Fornecedores
{

    public enum TipoRespostaModel
    {
        Eletiva = 1,
        Avaliacao = 2
    }

    public class FornecedorAvaliacaoRespostaModel
    {
        public int IdPergunta { get; set; }
        public string TextoPergunta { get; set; }
        public int IdForeceIdFornecedorQuestionarioResposta { get; set; }
        public double? Nota { get; set; }
        public bool? OpcaoEletiva { get; set; }
        public TipoRespostaModel TipoResposta { get; set; }
    }
}