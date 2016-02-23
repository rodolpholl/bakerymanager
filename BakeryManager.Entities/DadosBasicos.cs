using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Entities
{
    public class DadosBasicos
    {
        public virtual int IdDadosBasicos { get; set; }
        public virtual string NomeFantasia { get; set; }
        public virtual string RazaoSocial { get; set; }
        public virtual string CNPJ { get; set; }
        public virtual string InscricaoEstadual { get; set; }
        public virtual string Alvara { get; set; }
        public virtual string Logradouro { get; set; }
        public virtual string Numero { get; set; }
        public virtual string Complemento { get; set; }
        public virtual string Bairro { get; set; }
        public virtual string Cidade { get; set; }
        public virtual string UF { get; set; }
        public virtual string CEP { get; set; }
        public virtual double LatitudeMapa { get; set; }
        public virtual double LongitudeMapa { get; set; }
    }
}
