using BakeryManager.Entities.Seguranca.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Entities
{
    public class Perfil
    {
        [Required]
        public virtual int IdPerfil { get; set; }
        [Required]
        public virtual string Nome { get; set; }
        [Required]
        public virtual byte Atribuicao { get; set; }
        [Required]
        public virtual bool Ativo { get; set; }
    }
}
