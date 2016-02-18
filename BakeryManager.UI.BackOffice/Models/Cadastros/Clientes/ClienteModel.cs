using BakeryManager.InfraEstrutura.Helpers.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BakeryManager.BackOffice.Models.Cadastros.Clientes
{
    
    public class ClienteModel
    {
        public int IdCliente { get; set; }
        public string Nome { get; set; }
        [Display(Name = "Razão Social")]
        public string RazaoSocial { get; set; }
        [Display(Name = "Tipo de Cliente")]
        public TipoClienteModel TipoCliente { get; set; }

        [ValidadorCNPJ(ErrorMessage = "CNPJ Inválido! Verifique o valor informado e tente novamente")]
        public string CNPJ { get; set; }
        [ValidadorCPF(ErrorMessage = "CPF Inválido! Verifique o valor informado e tente novamente")]
        public string CPF { get; set; }


        [Display(Name = "Data de Aniversário")]
        public DateTime? DataAniversario { get; set; }

        [Display(Name = "Condição de Pagamento Preferencial")]
        public CondicaoPagamentoModel CondicaoPagamentoPreferencial { get; set; }
        public string Logradouro { get; set; }
        [Display(Name = "Número")]
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string CEP { get; set; }
        public bool Ativo { get; set; }
        public int IdTipoCliente { get; set; }
        public int IdCondicaoPagamento { get; set; }
    }
}