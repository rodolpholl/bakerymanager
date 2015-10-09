using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.BackOffice.Models.Cadastros
{
    public class CadastroUsuarioModel
    {
        [Required(ErrorMessage = "Campo Obrigatório")]
        public int IdUsuario { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Password { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
    }
}
