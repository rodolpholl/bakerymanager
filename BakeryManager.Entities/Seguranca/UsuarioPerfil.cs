using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Entities
{
    public class UsuarioPerfil
    {
        [Required]
        public virtual int IdUsuarioPerfil { get; set; }
        [Required]
        public virtual Usuario Usuario { get; set; }
        [Required]
        public virtual Perfil Perfil { get; set; }
        [Required]
        public virtual DateTime DataAssociacao { get; set; }
        [Required]
        public virtual bool Ativo { get; set; }
    }
}
