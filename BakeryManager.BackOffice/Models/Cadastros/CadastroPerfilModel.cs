using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BakeryManager.InfraEstrutura.Helpers;

namespace BakeryManager.BackOffice.Models.Cadastros
{
    public enum AtribuicaoPerfil
    {
        [Display(Description = "Adminstrador")]
        Administrador = 1,
        [Display(Description = "Operador")]
        Operador = 2,
        [Display(Description = "Cliente")]
        Cliente = 3
    }
    public class CadastroPerfilModel
    {

        [ScaffoldColumn(false)]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public  int IdPerfil { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Nome { get; set; }

        [ScaffoldColumn(false)]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public AtribuicaoPerfil AtribuicaoPerfilEnum { get; set; }

        
        [Display(Name = "Atribuição")]
        public  string Atribuicao { get
            {
                return AtribuicaoPerfilEnum.GetDescription();

            }
        }

        public bool Ativo { get; set; }
    }
}
