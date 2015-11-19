using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Entities
{
    public class Fornecedor
    {
        public virtual int IdFornecedor             { get; set; }
        public virtual string Nome                  { get; set; }
        public virtual string RazaoSocial           { get; set; }
        public virtual string CNPJ                  { get; set; }
        public virtual string Logradouro            { get; set; }
        public virtual string Numero                { get; set; }
        public virtual string Complemento           { get; set; }
        public virtual string Bairro                { get; set; }
        public virtual string Cidade                { get; set; }
        public virtual string UF                    { get; set; }
        public virtual string CEP                   { get; set; }
        public virtual bool Ativo                   { get; set; }
        public virtual int PrazoEntregaPrevisto     { get; set; }
        public virtual int QuantidadeEntregaSemana  { get; set; }
    }
}
