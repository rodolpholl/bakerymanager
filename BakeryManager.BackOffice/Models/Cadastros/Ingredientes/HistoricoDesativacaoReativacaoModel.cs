using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.BackOffice.Models.Cadastros
{
    public class HistoricoDesativacaoReativacaoModel
    {
        public int IdHistoricoDesativacaoReativacao { get; set; }

        [Display(Name = "Usuário Responsável")]
        public string UsuarioAtualizacao { get; set; }

        [Display(Name = "Tipoo de Operacao")]
        public string TipoOperacao { get; set; }

        [Display(Name = "Ip")]
        public string IpOperacao { get; set; }

        [Display(Name = "Data/Hora")]
        public DateTime DataHoraOperacao { get; set; }
    }
}
