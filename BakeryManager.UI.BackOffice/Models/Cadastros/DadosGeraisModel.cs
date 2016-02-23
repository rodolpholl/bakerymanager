

using BakeryManager.InfraEstrutura.Helpers.Validators;
using System.ComponentModel.DataAnnotations;

namespace BakeryManager.UI.BackOffice.Models.Cadastros
{
    public class DadosGeraisModel
    {
        public int IdDadosBasicos { get; set; }

        [Display(Name ="Nome")]
        [Required(ErrorMessage ="Campo Obrigatório!")]
        public string NomeFantasia { get; set; }

        [Display(Name = "Razão Social")]
        public string RazaoSocial { get; set; }

        [ValidadorCNPJ(ErrorMessage = "CNPJ Inválido! Verifique o valor informado e tente novamente")]
        public string CNPJ { get; set; }

        [Display(Name = "Inscrição Estadual")]
        public string InscricaoEstadual { get; set; }
        [Display(Name = "Alvará")]
        public string Alvara { get; set; }
        public string Logradouro { get; set; }
        [Display(Name="Número")]
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string CEP { get; set; }
        public double LatitudeMapa { get; set; }
        public double LongitudeMapa { get; set; }
    }
}
