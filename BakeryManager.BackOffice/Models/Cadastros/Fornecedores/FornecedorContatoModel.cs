using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BakeryManager.BackOffice.Models.Cadastros.Fornecedores
{
    public class FornecedorContatoModel
    {
        public  int IdFornecedorContato { get; set; }
        public  FornecedorModel Fornecedor { get; set; }
        public  string Nome { get; set; }
        public  string Telefone { get; set; }
        public  string Email { get; set; }
        public  string Site { get; set; }
    }
}