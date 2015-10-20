using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.BackOffice.Models.Cadastros.Ingredientes
{
    public class CategoriaIngredienteModel
    {
        public  int IdCategoriaIngrediente { get; set; }

        [Display(Name="Nome")]
        [Required(ErrorMessage ="Campo Obrigatório!")]
        [StringLength(200)]
        public string Nome { get; set; }

       

        public virtual bool PermiteExclusao { get; set; }

        public static implicit operator CategoriaIngredienteModel(CadastroIngredientesModel v)
        {
            return new CategoriaIngredienteModel();
        }
    }
}
