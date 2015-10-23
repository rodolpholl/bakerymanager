using BakeryManager.BackOffice.Models.Cadastros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.BackOffice.Models.ManterReceita
{
    public class IngredienteFormulaModel
    {
        public  int IdIngredienteFormula { get; set; }
        public  FormulaModel Formula { get; set; }
        public  IngredientesModel Ingrediente { get; set; }
        public  double Quantidade { get; set; }
        public  bool AGosto { get; set; }
    }
}
