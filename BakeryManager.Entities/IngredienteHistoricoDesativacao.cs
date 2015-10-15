using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Entities
{
    public enum TipoOpracaoDesativacaoIngrediente
    {
        Desativar = 1,
        Reativar = 2
    }
    public class IngredienteHistoricoDesativacao
    {
        public virtual int IdIngredienteHistoricoDesativacao { get; set; }
        public virtual Ingrediente Ingrediente { get; set; }
        public virtual Usuario UsuarioOperacao { get; set; }
        public virtual int TipoOperacao { get; set; }
        public virtual string IpOperacao { get; set; }
        public virtual DateTime DataHoraOperacao { get; set; }
    }
}
