using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Entities
{
    public class UsuarioPerfilHistorico
    {
        [Required]
        public virtual int IdUsuarioPerfilHistorico { get; set; }
        [Required]
        public virtual Usuario Usuario { get; set; }
        [Required]
        public virtual Perfil perfil { get; set; }
        [Required]
        public virtual DateTime DataAssociacao { get; set; }
        [Required]
        public virtual Usuario UsuarioAlteracao { get; set; }
    }
}
