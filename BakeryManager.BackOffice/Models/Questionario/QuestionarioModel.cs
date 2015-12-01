using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BakeryManager.BackOffice.Models.Questionario
{
    public class QuestionarioModel
    {
        public  int IdQuestionario { get; set; }
        public  string Nome { get; set; }
        [Display(Name ="Data de Criação")]
        public  DateTime DataCriacao { get; set; }
        [Display(Name = "Usar Prazo de Expiração?")]
        public  bool UsaPrazoExpiracao { get; set; }
        [Display(Name ="Prazo de Expiração (em dias)")]
        public  int PrazoExpiracao { get; set; }
        [Display(Name = "Data de Expiração")]
        public  DateTime? DataExpiracao { get; set; }
        public bool Ativo { get; set; }
    }
}