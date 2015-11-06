using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Entities
{
    public class ProdutoFoto
    {
        public virtual int IdProdutoFoto { get; set; }
        public virtual Produto Produto { get; set; }
        public virtual string NomeArquivo { get; set; }
        public virtual string NomeFisico { get; set; }
        public virtual string Extensao { get; set; }
        public virtual int Tamanho { get; set; }

    }
}
