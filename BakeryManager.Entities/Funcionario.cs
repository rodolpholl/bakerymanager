using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Entities
{
    public enum SituacaoFuncionario
    {
        Ativo = 1,
        Desligado = 2,
        Aposentado = 3,
        Falescido = 4
    }
    public class Funcionario
    {
        public virtual int IdFuncionario { get; set; }
        public virtual string Nome { get; set; }
        public virtual string CPF { get; set; }
        public virtual string RG { get; set; }
        public virtual string CTPS { get; set; }
        public virtual string Logradouro { get; set; }
        public virtual string Numero { get; set; }
        public virtual string Complemento { get; set; }
        public virtual string Bairro { get; set; }
        public virtual string Cidade { get; set; }
        public virtual string UF { get; set; }
        public virtual string CEP { get; set; }
        public virtual string TelefoneFixo { get; set; }
        public virtual string TelefoneCelular { get; set; }
        public virtual string Email { get; set; }
        public virtual DateTime DataInicioTrabalho { get; set; }
        public virtual double RemuneracaoAtual { get; set; }
        public virtual DateTime HorarioEntrada { get; set; }
        public virtual DateTime HorarioSaida { get; set; }
        public virtual SituacaoFuncionario SituacaoAtual { get; set; }
    }
}
