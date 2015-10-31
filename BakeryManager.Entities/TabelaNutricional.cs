using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Entities
{

    public enum ComponenteTACO
    {
        Umidade = 1,
        EnergiaKCAL = 2,
        EnergiaKJ = 3,
        Proteina = 4,
        Lipidio = 5,
        Colesterol = 6,
        Carbidrato = 7,
        FibraAlimentar = 9,
        Cinzas = 10,
        Calcio = 11,
        Magnesio = 12,
        Fosforo = 13,
        Ferro = 14,
        Manganes = 15,
        Sodio = 16,
        Potassio = 17,
        Cobre = 18,
        Zinco = 19,
        Retinol = 20,
        RE = 21,
        RAE = 22,
        Tiamina = 23,
        Piridoxina = 24,
        Riboflavina = 25,
        Niacina = 26,
        VitaminaC = 27
    }
 

    public class TabelaNutricional
    {
        public virtual int IdTabelaNutricional { get; set; }
        public virtual string Nome { get; set; }
        public virtual string UnidadeMedida { get; set; }
        public virtual double? ValorDiario { get; set; }
        
    }
}
