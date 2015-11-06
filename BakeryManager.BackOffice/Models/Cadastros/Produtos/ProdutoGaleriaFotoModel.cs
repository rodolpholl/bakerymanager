using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BakeryManager.BackOffice.Models.Cadastros.Produtos
{
    public class ProdutoGaleriaFotoModel
    {
        public int IdProdutoFoto { get; set; }
        public HttpPostedFileBase Arquivo { get; set; }
        public string NomeArquivo { get; set; }
        public string NomeFisico { get; set; }
        public  string Extensao { get; set; }
        public  int Tamanho { get; set; }

    }
}
