using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Entities
{
    public class RegistroAcesso
    {
        [Required]
        public virtual int IdRegistroAcesso { get; set; }
        [Required]
        public virtual Usuario Usuario { get; set; }
        [Required]
        public virtual DateTime DataHoraAcesso { get; set; }
        public virtual string Ip { get; set; }
        [Required]
        public virtual bool Sucesso { get; set; }
        public virtual bool NovoAcesso { get; set; }
        public virtual string DescritivoErro { get; set; }
    }
}
