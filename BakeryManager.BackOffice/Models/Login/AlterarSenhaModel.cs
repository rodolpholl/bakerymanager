using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BakeryManager.BackOffice.Models.Login
{
    public class AlterarSenhaModel
    {
        [Required(ErrorMessage = "* Campo obrigatório")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Sua senha deve conter minimamente 6 dígitos.")]
        [Display(Name = "Senha Atual")]
        public string SenhaAtual { get; set; }

        [Required(ErrorMessage = "* Campo obrigatório")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Sua senha deve conter minimamente 6 dígitos")]
        [MaxLength(15, ErrorMessage = "Sua senha deve conter 15 caracteres no mínimo.")]
        [Display(Name = "Nova senha")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "* Campo obrigatório")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Sua senha deve conter minimamente 6 dígitos")]
        [MaxLength(15, ErrorMessage = "Sua senha deve conter 15 caracteres no mínimo.")]
        [Display(Name = "Confirmar senha")]
        [Compare("NewPassword",ErrorMessage = "O Conteúdo deve ser idéntico ao digitado anteriormente. Verifique se os dois campos possuem o mesmo conteúdo.")]
        public string ConfirmNewPassword { get; set; }
    }
}