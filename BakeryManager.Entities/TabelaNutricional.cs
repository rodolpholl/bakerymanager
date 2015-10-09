using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Entities
{
    public class TabelaNutricional
    {
        public virtual int IdTabelaNutricional { get; set; }

        [Required(ErrorMessage = "O Ingrediente é Obrigatório!")]
        public virtual Ingrediente Ingrediente { get; set; }
        public virtual string Umidade { get; set; }
        public virtual string EnergiaKCAL { get; set; }
        public virtual string EnergiaKJ { get; set; }
        public virtual string Proteina { get; set; }
        public virtual string Lipidio { get; set; }
        public virtual string Colesterol { get; set; }
        public virtual string Carbidrato { get; set; }
        public virtual string FibraAlimentar { get; set; }
        public virtual string Cinzas { get; set; }
        public virtual string Calcio { get; set; }
        public virtual string Magnesio { get; set; }
        public virtual string Fosforo { get; set; }
        public virtual string Ferro { get; set; }
        public virtual string Manganes { get; set; }
        public virtual string Sodio { get; set; }
        public virtual string Potassio { get; set; }
        public virtual string Cobre { get; set; }
        public virtual string Zinco { get; set; }
        public virtual string Retinol { get; set; }
        public virtual string RE { get; set; }
        public virtual string RAE { get; set; }
        public virtual string Tiamina { get; set; }
        public virtual string Piridoxina { get; set; }
        public virtual string Riboflavina { get; set; }
        public virtual string Niacina { get; set; }
        public virtual string VitaminaC { get; set; }

    }
}
