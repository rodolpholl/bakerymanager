﻿using System;
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
        public virtual double Umidade { get; set; }
        public virtual double EnergiaKCAL { get; set; }
        public virtual double EnergiaKJ { get; set; }
        public virtual double Proteina { get; set; }
        public virtual double Lipidio { get; set; }
        public virtual double Colesterol { get; set; }
        public virtual double Carbidrato { get; set; }
        public virtual double FibraAlimentar { get; set; }
        public virtual double Cinzas { get; set; }
        public virtual double Calcio { get; set; }
        public virtual double Magnesio { get; set; }
        public virtual double Fosforo { get; set; }
        public virtual double Ferro { get; set; }
        public virtual double Manganes { get; set; }
        public virtual double Sodio { get; set; }
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
        public virtual double VitaminaC { get; set; }

    }
}
