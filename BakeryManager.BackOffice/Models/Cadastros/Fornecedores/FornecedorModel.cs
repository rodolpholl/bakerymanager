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
        public virtual bool Ativo { get; set; }
    }
}