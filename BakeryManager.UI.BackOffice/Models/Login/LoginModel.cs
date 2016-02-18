using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BakeryManager.BackOffice.Models.Login
{
    public class LoginModel
    {
        [Display(Name = "Login")]
        [Required(ErrorMessage = "* Campo obrigatório", AllowEmptyStrings = false)]
        public string Login { get; set; }

        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Mensagem Erro")]
        public string ErrorMensage { get; set; }

        [Display(Name ="Atribuição")]
        public int Atribuicao { get; set; }
    }
}