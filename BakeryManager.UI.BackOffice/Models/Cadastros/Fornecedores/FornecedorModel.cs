using BakeryManager.InfraEstrutura.Helpers.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace BakeryManager.BackOffice.Models.Cadastros.Fornecedores
{
    public class FornecedorModel
    {
        public int IdFornecedor { get; set; }
        [Required(ErrorMessage ="Campo Obrigatório!")]
        public string Nome { get; set; }
        [Display(Name = "Razão Social")]
        public string RazaoSocial { get; set; }
        [ValidadorCNPJ(ErrorMessage = "CNPJ Inválido! Verifique o valor informado e tente novamente")]
        public string CNPJ { get; set; }
        [Display(Name = "Logradouro")]
        public string Logradouro { get; set; }
        [Display(Name = "Número")]
        public string Numero { get; set; }
        [Display(Name = "Complemento")]
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string CEP { get; set; }
        public bool Ativo { get; set; }
        [Display(Name ="Prazo de entrega acordado (dias)")]
        public int PrazoEntregaPrevisto { get; set; }
        [Display(Name = "Número de entregas por semana")]
        public int QuantidadeEntregaSemana { get; set; }
    }
}