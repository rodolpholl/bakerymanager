using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BakeryManager.InfraEstrutura.Helpers.Validators;

namespace BakeryManager.BackOffice.Models.Cadastros.Fornecedores
{
    public class PerfilFornecedorUsuarioModel
    {
        public int IdPerfil { get; set; }
        public string Nome { get; set; }
    }
    public class FornecedorUsuarioModel
    {
        public int IdFornecedorUsuario { get; set; }

        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!")]
        public string Login { get; set; }

        [Display(Name ="E-mail")]
        [EmailAddress(ErrorMessage = "E-mail informado com formato inválido!")]
        public string Email { get; set; }

        [Display(Name ="Utilizar Email para Comunicação")]
        public bool UtilizaEmailComunicacao { get; set; }

        public bool HabilitaEdicao { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!")]
        public PerfilFornecedorUsuarioModel Perfil { get; set; }
    }
}
