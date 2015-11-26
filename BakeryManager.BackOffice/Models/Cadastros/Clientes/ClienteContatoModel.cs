using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BakeryManager.BackOffice.Models.Cadastros.Clientes
{
    public class ClienteContatoModel
    {
        public int IdClienteContato { get; set; }
        public ClienteModel Cliente { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Site { get; set; }
    }
}