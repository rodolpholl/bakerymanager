using BakeryManager.InfraEstrutura.Helpers.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BakeryManager.BackOffice.Models.Cadastros.Funcionarios
{
    public class FuncionarioModel
    {
        public int IdFuncionario { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        [ValidadorCPF(ErrorMessage = "O CPF informado é inválido!")]
        public string CPF { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string RG { get; set; }
        [Display(Name = "Carteira de Trabalho")]
        public string CTPS { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Logradouro { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Numero { get; set; }
        public string Complemento { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Bairro { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Cidade { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string UF { get; set; }
        public string CEP { get; set; }
        [Display(Name ="Telefone Fixo")]
        public string TelefoneFixo { get; set; }
        [Display(Name ="Telefone Celular")]
        public string TelefoneCelular { get; set; }
        [Display(Name = "E-Mail")]
        [EmailAddress(ErrorMessage = "E-mail Inválido")]
        public string Email { get; set; }
        [Display(Name = "Data de Início do Trabalho")]
        public DateTime DataInicioTrabalho { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        [Display(Name = "Remuneração Atual")]
        public double RemuneracaoAtual { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        [Display(Name = "Hora de Entrada")]
        public DateTime HorarioEntrada { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        [Display(Name = "Hora de Saída")]
        public DateTime HorarioSaida { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        [Display(Name = "Situação Atual")]
        public SituacaoFucionarioModel SituacaoAtual { get; set; }
        [Display(Name ="Possui acesso ao sistema?")]
        public bool PossuiAcessoSistema { get; set; }
        [Display(Name = "Login")]
        public string Login { get; set; }
        [Display(Name = "Usa Senha do Dia?")]
        public bool UsaSenhaDia { get; set; }
        [Display(Name ="Perfil")]
        public int IdPefil { get; set; }
    }

    public class SituacaoFucionarioModel
    {
        public int IdSituacaoFuncionario { get; set; }
        public string Descricao { get; set; }
    }
}