using BakeryManager.BackOffice.Models.Cadastros.Ingredientes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.BackOffice.Models.Cadastros
{
    public class CadastroIngredientesModel
    {

        public int IdIngrediente { get; set; }

        [Required(ErrorMessage ="Campo Obrigatório!")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Nome TACO")]
        public string NomeTACO { get; set; }
        
        [Display(Name = "Abreviatura")]
        public string Abreviatura { get; set; }
        [Display(Name = "Cód. TACO" )]
        public int? CodigoTACO { get; set; }
        public bool Ativo { get; set; }
        public TabelaNutricionalModel TabelaNutricional { get; set; }

        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public CategoriaIngredienteModel Categoria { get; set; }

        public CadastroIngredientesModel()
        {
            TabelaNutricional = new TabelaNutricionalModel()
            {
                Ingrediente = this
            };

            Categoria = new CategoriaIngredienteModel();
            
        }



    }
}
