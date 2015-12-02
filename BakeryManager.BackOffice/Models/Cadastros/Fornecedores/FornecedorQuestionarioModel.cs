using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BakeryManager.BackOffice.Models.Cadastros.Fornecedores
{
    public class FornecedorQuestionarioConfigModel
    {
        public  String Questionario { get; set; }
        public bool Selecionado { get; set; }
        public int IdQuestionario { get; set; }
        
    }
}