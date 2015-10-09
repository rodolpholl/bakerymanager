using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Entities
{
    public class UsuarioHistoricoSenha
    {
        [Required]
        public virtual int IdUsuarioHistoricoSenha { get; set; }
        [Required]
        public virtual Usuario Usuario { get; set; }
        [Required]
        public virtual string Login { get; set; }
        [Required]
        public virtual string Senha { get; set; }
        [Required]
        public virtual DateTime DataInclusao { get; set; }

    }
}
