using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.BackOffice.Models.Cadastros.Produtos
{
    public class IngredienteTabelaNutricionalProduto
    {
        public int IdIngredienteTabelaNutricional { get; set; }
        public string Ingrediente { get; set; }
        public double? Valor { get; set; }
        [Display(Name ="% VD")]
        public double? PercValorDiario { get; set; }
        public string UnidadeMedida { get; set; }
        
    }
}
