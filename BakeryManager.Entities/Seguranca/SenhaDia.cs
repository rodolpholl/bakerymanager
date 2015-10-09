using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Entities
{
    public class SenhaDia
    {
        [Required]
        public virtual int IdSenhaDia { get; set; }
        [Required]
        public virtual string MascaraSenhaDia { get; set; }
        [Required]
        public virtual DateTime DataCriacao { get; set; }
        [Required]
        public virtual bool Ativo { get; set; }
    }
}
