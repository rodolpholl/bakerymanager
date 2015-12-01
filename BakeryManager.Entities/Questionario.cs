using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Entities
{
    public class Questionario
    {
        public virtual int IdQuestionario { get; set; }
        public virtual string Nome { get; set; }
        public virtual DateTime DataCriacao { get; set; }
        public virtual bool UsaPrazoExpiracao { get; set; }
        public virtual int PrazoExpiracao { get; set; }
        public virtual DateTime? DataExpiracao { get; set; }
        public virtual bool Ativo { get; set; }
    }
}
