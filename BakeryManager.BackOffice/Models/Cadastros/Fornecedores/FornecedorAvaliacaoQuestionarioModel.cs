using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BakeryManager.BackOffice.Models.Cadastros.Fornecedores
{
    public class FornecedorAvaliacaoQuestionarioModel
    {
        public int IdFornecedorAvaliacaoQuestionario { get; set; }
        public int IdQuestionario { get; set; }
        [Display(Name = "Avaliação")]
        public string Nome { get; set; }
        public IList<FornecedorAvaliacaoRespostaModel> Respostas { get; set; }
        
    }
}