using BakeryManager.BackOffice.Models.Cadastros;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.BackOffice.Models.ManterReceita
{
    public class IngredienteFormulaModel
    {
        public  int IdIngredienteFormula { get; set; }
        public  int IdFormula { get; set; }
        public  int  IdIngrediente { get; set; }
        public string Nome { get; set; }
        [Required(ErrorMessage ="Campo Obrigatório")]
        public  double Quantidade { get; set; }
        [Display(Name = "A Gosto?")]
        public  bool AGosto { get; set; }
    }
}
