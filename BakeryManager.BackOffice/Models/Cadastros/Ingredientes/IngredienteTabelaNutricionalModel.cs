using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.BackOffice.Models.Cadastros.Ingredientes
{
    public class IngredienteTabelaNutricionalModel
    {
        public int IdIngredienteTabelaNutricional { get; set; }

     
        [Display(Name = "Ingrediente")]
        public CadastroIngredientesModel Ingrediente { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!")]
        [Display(Name = "Componente Nutricional")]
        public TabelaNutricionalModel ComponenteNutricional { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!")]
        [Display(Name = "Valor")]
        public double Valor { get; set; }

    }
}
