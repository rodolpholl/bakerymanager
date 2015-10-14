using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.BackOffice.Models.Cadastros.Ingredientes
{
    public class TabelaNutricionalModel
    {
        [Required(ErrorMessage = "Campo Obrigatório!")]
        [Display(Name = "Ingrediente")]
        public CadastroIngredientesModel Ingrediente { get; set; }

        public virtual double Umidade { get; set; }
        [Display(Name = "Ener. KCAL")]
        public virtual double EnergiaKCAL { get; set; }
        [Display(Name = "Ener. KJ")]
        public virtual double EnergiaKJ { get; set; }
        [Display(Name = "Proteína")]
        public virtual double Proteina { get; set; }
        [Display(Name = "Lipídio")]
        public virtual double Lipidio { get; set; }
        public virtual double Colesterol { get; set; }
        public virtual double Carbidrato { get; set; }
        [Display(Name = "Fibra Alimentar")]
        public virtual double FibraAlimentar { get; set; }
        public virtual double Cinzas { get; set; }
        [Display(Name = "Cálcio")]
        public virtual double Calcio { get; set; }
        [Display(Name = "Magnésio")]
        public virtual double Magnesio { get; set; }
        [Display(Name = "Fósforo")]
        public virtual double Fosforo { get; set; }
        public virtual double Ferro { get; set; }
        [Display(Name = "Manganês")]
        public virtual double Manganes { get; set; }
        [Display(Name = "Sódio")]
        public virtual double Sodio { get; set; }
        [Display(Name = "Potácio")]
        public virtual double Potassio { get; set; }
        public virtual double Cobre { get; set; }
        public virtual double Zinco { get; set; }
        public virtual double Retinol { get; set; }
        public virtual double RE { get; set; }
        public virtual double RAE { get; set; }
        public virtual double Tiamina { get; set; }
        public virtual double Piridoxina { get; set; }
        public virtual double Riboflavina { get; set; }
        public virtual double Niacina { get; set; }
        [Display(Name = "Vitamina C")]
        public virtual double VitaminaC { get; set; }
    }
}
