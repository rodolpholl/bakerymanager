using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Entities
{
    public class Usuario
    {
        [Required]
        public virtual int IdUsuario { get; set; }
        [Required(ErrorMessage = "Caampo Obrigatório!")]
        [MinLength(4, ErrorMessage = "Esta campo precisa ter ao menos 4 caracteres")]
        public virtual string Nome { get; set; }
        [Required(ErrorMessage = "Caampo Obrigatório!")]
        [MinLength(4,ErrorMessage = "Esta campo precisa ter ao menos 4 caracteres")]
        public virtual string Login { get; set; }

        [MinLength(4, ErrorMessage = "Esta campo precisa ter ao menos 4 caracteres")]
        public virtual string Password { get; set; }
        [Required]
        public virtual DateTime DataCriacao { get; set; }
        [Required]
        public virtual DateTime? DataUltimoLogin { get; set; }
        [Required]
        public virtual bool Ativo { get; set; }
        [Required]
        public virtual bool AutenticaSenhaDia { get; set; }

        public virtual string Email { get; set; }

        public virtual string Telefone { get; set; }
    }
}
