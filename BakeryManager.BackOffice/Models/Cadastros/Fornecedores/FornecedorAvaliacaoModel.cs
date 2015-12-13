using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BakeryManager.BackOffice.Models.Cadastros.Fornecedores
{
    public class FornecedorAvaliacaoModel
    {
        public int IdFornecedorAvaliacao { get; set; }
        public int IdFornecedor { get; set; }
        [StringLength(4000)]
        [DataType(DataType.Html)]
        [AllowHtml]
        [Display(Name ="Observação")]
        public string Observacao { get; set; }
        public string UsuarioAteracao { get; set; }
        public string IpAlteracao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public bool HabilitaEdicao { get; set; }
    }
}