using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BakeryManager.InfraEstrutura.Helpers;

namespace BakeryManager.BackOffice.Models.Cadastros
{
    public class AtribuicaoPerfilModel
    {

        public int IdAtribuicaoPerfil { get; set; }
        public string Nome { get; set; }

        
    }
    

    public class CadastroPerfilModel
    {

        [ScaffoldColumn(false)]
        public  int IdPerfil { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Nome { get; set; }
        
        [Display(Name = "Atribuição")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public AtribuicaoPerfilModel Atribuicao { get; set; }

        public bool Ativo { get; set; }

        public CadastroPerfilModel()
        {
            Atribuicao = new AtribuicaoPerfilModel();
        }
    }
}
